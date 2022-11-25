using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class ActiveDefenderAgent : Agent
	{
        [SerializeField] private bool isTrainingMode;
        [SerializeField] private Vector3 maxPos;
        private float dmgInflicted;
        private const float rangeMultiply = 128f;
        private string dmgKey;
        private Vector3 lastPos, lastAgentMapPos, lastEnemyMapPos;
        private NavMeshAgent navAgent;
        private ActiveDefenderStateManager mlManager;

        private void Awake()
        {
            mlManager = GetComponent<ActiveDefenderStateManager>();
            navAgent = GetComponent<NavMeshAgent>(); 
            dmgKey = transform.name + transform.GetInstanceID();
        }
        private bool IsChangeRelevant(Vector3 newPos)
        {
            return (lastPos - newPos).sqrMagnitude > 9f;
        }
        private void SetAgentDestination(Vector3 dir)
        {
            Vector3 pos = (dir + mlManager.AgentTransform.position);

            if (!IsChangeRelevant(pos))
                return;

            lastPos = pos;

            if (Physics.Raycast(pos, -Vector3.up * pos.y, out RaycastHit hit, mlManager.GetFSMSettings().sightRange))
                pos = hit.point;

            if (NavMesh.SamplePosition(pos, out NavMeshHit navHit, GameConfig.wanderTargetRandomRadius, NavMesh.AllAreas))
            {
                navAgent.SetDestination(navHit.position);
            }
        }
        public override void OnEpisodeBegin()
        {
            bool isSearchPos = true;
            int numAttempts = 0;
            while (isSearchPos && numAttempts < 100)
            {
                Vector3 rndPos = new Vector3(
                    Random.Range(5f, 128f),
                    1f,
                    Random.Range(5f, 128f)
                );
                if (Physics.Raycast(rndPos, -Vector3.up * rndPos.y, out RaycastHit hit, mlManager.GetFSMSettings().sightRange))
                    rndPos = hit.point;
                if (NavMesh.SamplePosition(rndPos, out NavMeshHit navHit, GameConfig.wanderTargetRandomRadius, NavMesh.AllAreas))
                {
                    navAgent.SetDestination(navHit.position);
                    isSearchPos = false;
                }
                numAttempts++;
            }
        }
        private void SetOnMapPositions()
        {
            Vector3 aPos = mlManager.AgentTransform.position;
            if (aPos != lastAgentMapPos) 
            {
               mlManager.AgentObservations.AgentMapPosition = new Vector3(
                    aPos.x / maxPos.x,
                    aPos.y / maxPos.y,
                    aPos.z / maxPos.z
               );
               Debug.Log("Set agent position to: " + mlManager.AgentObservations.AgentMapPosition + " max pos " + maxPos);
               lastAgentMapPos = aPos;
            }
           
            Vector3 ePos = mlManager.AgentObservations.EnemyMapPosition;
            if (ePos != lastEnemyMapPos)
            {
                mlManager.AgentObservations.EnemyMapPosition = new Vector3(
                    ePos.x / maxPos.x,
                    ePos.y / maxPos.y,
                    ePos.z / maxPos.z
                );
                lastEnemyMapPos = ePos;
            }
        }
        public override void CollectObservations(VectorSensor sensor)
        {
            SetOnMapPositions();

            sensor.AddObservation(mlManager.AgentObservations.NumOfVisibleEnemies);
            sensor.AddObservation(mlManager.AgentObservations.AgentMapPosition);
            sensor.AddObservation(mlManager.AgentObservations.EnemyMapPosition);
            sensor.AddObservation(mlManager.AgentObservations.SpottedEnemyMapPosition);
        }
        private float GetLastInflictedDamage()
        {
            float currentDmg = GlobalDamageMaster.GetDamageForEntity(dmgKey);

            if (currentDmg <= dmgInflicted)
            {
                dmgInflicted = currentDmg;
                return 0f;
            }

            float dmg = currentDmg - dmgInflicted;
            dmgInflicted = currentDmg;
            return dmg;
        }
        private void CalculateReward()
        {
            float dmg = GetLastInflictedDamage();

            if (dmg <= 0f)
                AddReward(-0.00025f);
            else
                AddReward(dmg * 0.01f);
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            ActionSegment<float> conActions = actions.ContinuousActions;

            Vector3 moveDir = new Vector3(conActions[0], conActions[1], conActions[2]) * (rangeMultiply * conActions[3]);
            SetAgentDestination(moveDir);
            CalculateReward();
        }
    }
}
