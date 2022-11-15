using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.AI;
namespace URPMk2
{
	public class InceptorAgent : Agent, IMLAgent
	{
		[SerializeField] private bool isTrainingMode;
		private float rangeMultiply = 64f;
		private NavMeshAgent navAgent;
		private MLStateManager mlManager;
		private InceptorRewardCalculator rewardCalculator;

		private void Awake()
		{
			mlManager = GetComponent<MLStateManager>();
            navAgent = GetComponent<NavMeshAgent>();
			rewardCalculator = new InceptorRewardCalculator(this, transform);
        }
		private void Start()
		{
            rangeMultiply += (mlManager.AgentTransform.position.x + mlManager.AgentTransform.position.z) / 2f;
        }
		private void SetAgentDestination(Vector3 dir, float range)
		{
			Vector3 pos = (dir + mlManager.AgentTransform.position) * range;

            
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
			Vector3 maxPos = GameConfig.maxMapDim;
            mlManager.AgentObservations.agentMapPosition = new Vector3(
				aPos.x / maxPos.x,
				aPos.y / maxPos.y,
				aPos.z / maxPos.z
                );
		}
		public override void CollectObservations(VectorSensor sensor)
		{
			SetOnMapPosition();

            sensor.AddObservation(mlManager.AgentObservations.agentMapPosition);
            sensor.AddObservation(mlManager.AgentObservations.numOfVisibleEnemies);
			sensor.AddObservation(mlManager.AgentObservations.distanceToEnemy);
			sensor.AddObservation(mlManager.AgentObservations.enemyDirection);
			sensor.AddObservation(mlManager.AgentObservations.spottedEnemyDirection);
        }
        public override void OnActionReceived(ActionBuffers actions)
		{
            ActionSegment<float> conActions = actions.ContinuousActions;

			Vector3 moveDir = new Vector3(conActions[0], conActions[1], conActions[2]);
			float range = conActions[3] * rangeMultiply;

            SetAgentDestination(moveDir, range);
        }
		public void PassReward()
		{
			if (!isTrainingMode)
				return;

			float[] rewardPenalty = rewardCalculator.GetReward();
			AddReward(rewardPenalty[0]);
			AddReward(rewardPenalty[1]);
		}
    }
}
