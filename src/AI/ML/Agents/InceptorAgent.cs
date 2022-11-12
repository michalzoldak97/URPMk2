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
		private const float rangeMultiply = 256f;
		private Vector3 lastDir;
		private NavMeshAgent navAgent;
		private MLStateManager mlManager;
		private InceptorRewardCalculator rewardCalculator;

		private void Awake()
		{
			mlManager = GetComponent<MLStateManager>();
            navAgent = GetComponent<NavMeshAgent>();
			rewardCalculator = new InceptorRewardCalculator(transform);
		}
		private bool IsChnageSiginificant(Vector3 pos)
		{
			if ((pos - lastDir).sqrMagnitude < 1)
				return false;

			return true;
		}
		private bool IsPlaceDifferent(Vector3 pos) // disable for precision fight
		{
            if ((pos - lastDir).sqrMagnitude < 400)
                return false;

            return true;
        }
		private void SetAgentDestination(Vector3 dir, float range)
		{
			Vector3 pos = (dir + transform.position) * range;

            if (!IsChnageSiginificant(pos))
                return;

            AddReward(0.0001f);

			if (IsPlaceDifferent(pos)) // encurage longer distances for the begining
				AddReward(0.0002f);

            lastDir = pos;

            if (Physics.Raycast(pos, -Vector3.up * pos.y, out RaycastHit hit, mlManager.GetFSMSettings().sightRange))
                pos = hit.point;

            if (NavMesh.SamplePosition(pos, out NavMeshHit navHit, GameConfig.wanderTargetRandomRadius, NavMesh.AllAreas))
			{
                navAgent.SetDestination(navHit.position);
				AddReward(0.0001f);
            }
		}
		public override void OnEpisodeBegin()
		{
			bool isSearchPos = true;
			int numAttempts = 0;
			while (isSearchPos && numAttempts < 100)
			{
                Vector3 rndPos = new Vector3(
					Random.Range(5f, 256f),
					1f,
					Random.Range(5f, 256f)
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
		public override void CollectObservations(VectorSensor sensor)
		{
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
