using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class SquadDefenderAgent : Agent
	{
        private int idleCount;
        private const float rangeMultiply = 512f;
        private float dmgInflicted = 0f;
        private string dmgKey;
        private Vector3 lastPos, emptyObservation;
        private NavMeshAgent navAgent;
        private SquadDefenderStateManager mlManager;
        private DamagableMaster dmgMaster;

        private void Awake()
        {
            mlManager = GetComponent<SquadDefenderStateManager>();
            navAgent = GetComponent<NavMeshAgent>();
            dmgMaster = GetComponent<DamagableMaster>();
            emptyObservation = new Vector3(-1f, -1f, -1f);
            dmgKey = transform.name + transform.GetInstanceID();
        }
        private void Start()
        {
            mlManager.AgentObservations.SpottedTarget = emptyObservation;
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

            if ((lastPos - pos).sqrMagnitude < 4f)
                return;

            lastPos = pos;

            SetAvilableDestination(pos);
        }
        private Vector2 GetCargoPosition()
        {

            if (mlManager.MyFollowTarget == null)
                return new Vector2(-1f, -1f);

            Vector3 rPos = (mlManager.MyFollowTarget.position - mlManager.AgentTransform.position);
            return new Vector2(
                Vector3.Dot(rPos.normalized, mlManager.AgentTransform.forward),
                (rPos.magnitude / rangeMultiply));
        }
        private Vector2 GetEnemyPosition(Vector3 tPos)
        {
            if (tPos == emptyObservation)
                return new Vector2(-1f, -1f);

            Vector3 rPos = (tPos - mlManager.AgentTransform.position);
            return new Vector2(
                Vector3.Dot(rPos.normalized, mlManager.AgentTransform.forward),
                (rPos.magnitude / rangeMultiply));
        }
        private float GetFollowTargetHealth()
        {
            if (mlManager.MyFollowTarget == null ||
                !mlManager.MyFollowTarget.gameObject.activeSelf)
                return 0f;

            return mlManager.MyFollowTarget.GetComponent<DamagableMaster>().GetHealth();
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
        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(GetCargoPosition());
            sensor.AddObservation(GetEnemyPosition(mlManager.AgentObservations.AttackTarget));
            sensor.AddObservation(GetEnemyPosition(mlManager.AgentObservations.SpottedTarget));
            sensor.AddObservation(GetFollowTargetHealth() / 1200f);
            float lastDmg = GetLastInflictedDamage() * 0.01f;
            sensor.AddObservation(lastDmg);

            /*if (lastDmg != 0f)
                Debug.Log("Cargo Pos "
                    + GetCargoPosition().x + ", " + GetCargoPosition().y
                    + " Enemy Pos " + GetEnemyPosition(mlManager.AgentObservations.AttackTarget).x + ", " + GetEnemyPosition(mlManager.AgentObservations.AttackTarget).y
                    + " Spotted Pos " + GetEnemyPosition(mlManager.AgentObservations.SpottedTarget).x + ", " + GetEnemyPosition(mlManager.AgentObservations.SpottedTarget).y
                    + " health " + GetFollowTargetHealth()
                    + " dmg " + lastDmg
                    + " reward " + GetCumulativeReward());*/
        }
        private void CalculateReward()
        {
            float dist = Vector3.Distance(
                    mlManager.MyFollowTarget.position,
                    mlManager.AgentTransform.position);
            if (dist < 35f)
            {
                AddReward(0.005f);
            }
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            ActionSegment<float> conActions = actions.ContinuousActions;

            Vector2 moveDir;
            moveDir.x = conActions[0] * rangeMultiply;
            moveDir.y = conActions[1] * rangeMultiply;

            SetAgentDestination(moveDir);

            CalculateReward();
        }
    }
}
