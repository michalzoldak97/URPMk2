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
        [SerializeField] private Vector3 maxPos;
        private float dmgInflicted, health, initHealth;
        private const float rangeMultiply = 128f;
        private string dmgKey;
        private Vector3 lastPos, lastAgentMapPos, lastEnemyMapPos, lastSpottedMapPos, emptyInput;
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
        private void SetOnMapPosition()
        {
            Vector3 aPos = mlManager.AgentTransform.position;
            if (aPos != lastAgentMapPos)
            {
                mlManager.AgentObservations.AgentMapPosition = new Vector3(
                     aPos.x / maxPos.x,
                     aPos.y / maxPos.y,
                     aPos.z / maxPos.z
                );
                lastAgentMapPos = aPos;
            }

            Vector3 ePos = mlManager.AgentObservations.EnemyMapPosition;
            if (ePos != emptyInput &&
                ePos != lastEnemyMapPos)
            {
                mlManager.AgentObservations.EnemyMapPosition = new Vector3(
                    ePos.x / maxPos.x,
                    ePos.y / maxPos.y,
                    ePos.z / maxPos.z
                );
                lastEnemyMapPos = ePos;
            }
            Vector3 sPos = mlManager.AgentObservations.SpottedEnemyMapPosition;
            if (sPos != emptyInput &&
                sPos != lastSpottedMapPos)
            {
                mlManager.AgentObservations.SpottedEnemyMapPosition = new Vector3(
                    sPos.x / maxPos.x,
                    sPos.y / maxPos.y,
                    sPos.z / maxPos.z
                );
                lastSpottedMapPos = sPos;
            }
        }
        public override void CollectObservations(VectorSensor sensor)
        {
            SetOnMapPosition();

            sensor.AddObservation(health / initHealth);
            sensor.AddObservation(mlManager.AgentObservations.AgentMapPosition);
            sensor.AddObservation(mlManager.AgentObservations.EnemyMapPosition);
            sensor.AddObservation(mlManager.AgentObservations.SpottedEnemyMapPosition);

            gManager.UpdateObservations(
                mlManager.AgentObservations.AgentMapPosition, 
                mlManager.AgentObservations.EnemyMapPosition, 
                mlManager.AgentObservations.SpottedEnemyMapPosition,
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
            ActionSegment<float> conActions = actions.ContinuousActions;

            Vector3 moveDir = new Vector3(conActions[0], conActions[1], conActions[2]) * (rangeMultiply * conActions[3]);
            SetAgentDestination(moveDir);
            CalculateReward();
        }
    }
}
