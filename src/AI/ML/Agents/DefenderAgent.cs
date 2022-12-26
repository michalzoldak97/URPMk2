using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace URPMk2
{
	public class DefenderAgent : Agent, IGAILAgent
    {
        [SerializeField] private bool isHeuristic;
        [SerializeField] private Vector3 maxPos;
        private int idleCount;
        private float dmgInflicted, reward;
        private const float rangeMultiply = 512f;
        private string dmgKey;
        private Vector3 lastPos, emptyInput, hDestination;
        private NavMeshAgent navAgent;
        private DefenderStateManager mlManager;
        private GAILControlPanelManager gManager;
        private DamagableMaster dmgMaster;

        public void SetGAILManager(GAILControlPanelManager gManager)
        {
            this.gManager = gManager;
        }
        private void Awake()
        {
            mlManager = GetComponent<DefenderStateManager>();
            navAgent = GetComponent<NavMeshAgent>();
            dmgKey = transform.name + transform.GetInstanceID();
            dmgMaster = GetComponent<DamagableMaster>();
            emptyInput = new Vector3(-1f, -1f, -1f);
        }
        private void Start()
        {
            int observationsCount = mlManager.AgentObservations.SpottedEnemyMapPositions.Length;
            Vector3 emptyObservation = new Vector3(-1f, -1f, -1f);
            for (int i = 0; i < observationsCount; i++)
            {
                mlManager.AgentObservations.SpottedEnemyMapPositions[i] = emptyObservation;
            }
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            dmgMaster.EventDestroyObject += OnAgentDestroy;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            dmgMaster.EventDestroyObject -= OnAgentDestroy;
        }

        private void OnAgentDestroy(Transform killer)
        {
            EndEpisode();
        }
        private void SetRandomDestination()
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
        private void SetAvilableDestination(Vector3 pos)
        {
            if (Physics.Raycast(pos, -Vector3.up * pos.y, out RaycastHit hit, mlManager.GetFSMSettings().sightRange))
            {
                pos = hit.point;
                if (NavMesh.SamplePosition(pos, out NavMeshHit navHit, GameConfig.wanderTargetRandomRadius, NavMesh.AllAreas))
                {
                    navAgent.SetDestination(navHit.position);
                    idleCount = 0;
                    return;
                }
                else
                    idleCount++;
            }
            else
                idleCount++;

            if (idleCount > 100)
            {
                SetRandomDestination();
                idleCount = 0;
            }
        }
        private void SetAgentDestination(Vector2 dir)
        {
            Vector3 pos = mlManager.AgentTransform.position;
            pos.x += dir.x;
            pos.z += dir.y;

            if ((lastPos - pos).sqrMagnitude < 1f)
                return;

            lastPos = pos;

            SetAvilableDestination(pos);
        }
        private Vector2 GetOnMapPosition(Vector3 inVec)
        {
            if (inVec == emptyInput)
                return inVec;

            Vector2 outVec = new Vector2(
                    inVec.x / maxPos.x,
                    inVec.z / maxPos.z
                );

            return outVec;
        }
        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(mlManager.AgentObservations.NumOfVisibleEnemies);
            sensor.AddObservation(GetOnMapPosition(mlManager.AgentObservations.CargoParentMapPosition));
            sensor.AddObservation(GetOnMapPosition(mlManager.AgentTransform.position));
            sensor.AddObservation(GetOnMapPosition(mlManager.AgentObservations.EnemyMapPosition));
            int spottedEnemyCount = mlManager.AgentObservations.SpottedEnemyMapPositions.Length;
            for (int i = 0; i < spottedEnemyCount; i++)
            {
                sensor.AddObservation(GetOnMapPosition(mlManager.AgentObservations.SpottedEnemyMapPositions[i]));
            }
            

            if (isHeuristic)
                gManager.UpdateObservations(
                    GetOnMapPosition(mlManager.AgentTransform.position),
                    GetOnMapPosition(mlManager.AgentObservations.EnemyMapPosition),
                    GetOnMapPosition(mlManager.AgentObservations.CargoParentMapPosition),
                    mlManager.AgentObservations.NumOfVisibleEnemies,
                    reward);
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

            if (dmg > 0f)
            {
                AddReward(dmg * 0.01f);
                reward += dmg * 0.01f;
            }

            if (Vector3.Distance(
                    mlManager.AgentObservations.CargoParentMapPosition,
                    mlManager.AgentTransform.position) < 15f)
            {
                AddReward(0.0001f);
                reward += 0.0001f;
            }

            float cargoDmg = mlManager.AgentObservations.CargoParentDamage;
            if (cargoDmg > 0)
            {
                AddReward(cargoDmg * -0.0025f);
                reward += cargoDmg * -0.0025f;
                mlManager.AgentObservations.CargoParentDamage = 0f;
            }
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            ActionSegment<float> conActions = actions.ContinuousActions;

            Vector2 moveDir = new Vector2(conActions[0], conActions[1]) * rangeMultiply;
            SetAgentDestination(moveDir);

            CalculateReward();
        }
        public void SetHDestination(Vector3 des)
        {
            hDestination = des;
        }
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            Vector3 pos = mlManager.AgentTransform.position;
            ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;

            continuousActionsOut[0] = (hDestination.x - pos.x) / rangeMultiply;
            continuousActionsOut[1] = (hDestination.z - pos.z) / rangeMultiply;
        }
        public void OnAgentWon()
        {
            EndEpisode();
        }
    }
}
