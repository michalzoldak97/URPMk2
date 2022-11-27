using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
    public class InterceptorGAILAgent : Agent
    {
        [SerializeField] private bool isTrainingMode;
        [SerializeField] private bool isHeuristic;
        [SerializeField] private Vector3 maxPos;
        private bool isMoveAction;
        private float dmgInflicted, health, initHealth;
        private const float rangeMultiply = 128f;
        private string dmgKey;
        private Vector3 lastPos, lastAgentMapPos, lastEnemyMapPos, lastSpottedMapPos, emptyInput, hDestination;
        private NavMeshAgent navAgent;
        private InterceptorStateManager mlManager;
        private GAILControlPanelManager gManager;
        private DamagableMaster dmgMaster;

        public void SetGAILManager(GAILControlPanelManager gManager)
        {
            this.gManager = gManager;
        }
        private void Awake()
        {
            mlManager = GetComponent<InterceptorStateManager>();
            navAgent = GetComponent<NavMeshAgent>();
            dmgKey = transform.name + transform.GetInstanceID();
            dmgMaster = GetComponent<DamagableMaster>();
            initHealth = 100f;
            health = 100f;
            emptyInput = new Vector3(-1f, -1f, -1f);
        }
        private void Start()
        {
            initHealth = dmgMaster.GetHealth();
            health = dmgMaster.GetHealth();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            dmgMaster.EventReceivedDamage += OnAgentDamage;
            dmgMaster.EventDestroyObject += OnAgentDestroy;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            dmgMaster.EventReceivedDamage -= OnAgentDamage;
            dmgMaster.EventDestroyObject -= OnAgentDestroy;
        }
        private void OnAgentDamage(Transform origin, float dmg)
        {
            health -= dmg;
            if (health <= 0)
                health = 1f;
        }
        private void OnAgentDestroy(Transform killer)
        {
            EndEpisode();
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
        private Vector3 GetOnMapPosition(Vector3 inVec)
        {
            if (inVec == emptyInput)
                return inVec;

            Vector3 outVec = new Vector3(
                    inVec.x / maxPos.x,
                    inVec.y / maxPos.y,
                    inVec.z / maxPos.z
                );

            return outVec;
        }
        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(health / initHealth);
            sensor.AddObservation(GetOnMapPosition(mlManager.AgentTransform.position));
            sensor.AddObservation(GetOnMapPosition(mlManager.AgentObservations.EnemyMapPosition));
            sensor.AddObservation(GetOnMapPosition(mlManager.AgentObservations.SpottedEnemyMapPosition));

            if (isHeuristic)
                gManager.UpdateObservations(
                    GetOnMapPosition(mlManager.AgentObservations.AgentMapPosition),
                    GetOnMapPosition(mlManager.AgentObservations.EnemyMapPosition),
                    GetOnMapPosition(mlManager.AgentObservations.SpottedEnemyMapPosition),
                    health / initHealth);
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
                AddReward(-0.00001f);
            else
                AddReward(dmg * 0.01f);
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            if (!isHeuristic)
            {
                ActionSegment<float> conActions = actions.ContinuousActions;

                Vector3 moveDir = new Vector3(conActions[0], conActions[1], conActions[2]) * (rangeMultiply * conActions[3]);
                SetAgentDestination(moveDir);
            }
            CalculateReward();
        }
        public void SetHDestination(Vector3 des)
        {
            hDestination = des;
            isMoveAction = true;
        }
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            if (!isMoveAction)
                return;

            if (Physics.Raycast(hDestination, -Vector3.up * hDestination.y, out RaycastHit hit, mlManager.GetFSMSettings().sightRange))
                hDestination = hit.point;

            if (NavMesh.SamplePosition(hDestination, out NavMeshHit navHit, 1f, NavMesh.AllAreas))
                navAgent.SetDestination(navHit.position);

            isMoveAction = false;
        }
        public void OnAgentWon()
        {
            AddReward(1f);
            EndEpisode();
        }
    }
}
