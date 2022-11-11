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
		private const float dirThreshold = 0.0025f;
        private const float rangeThreshold = 3f;
		private const float rangeMultiply = 10f;
        private float lastRange;
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
		private bool IsChnageSiginificant(Vector3 dir, float range)
		{
            for (int i = 0; i < 3; i++)
			{
				if (Mathf.Abs(dir[i] - lastDir[i]) > dirThreshold)
					return true;
			}
			if (Mathf.Abs(range - lastRange) > rangeThreshold)
				return true;

			return false;
		}
		private void SetAgentDestination(Vector3 dir, float range)
		{
			/*if (!IsChnageSiginificant(dir, range))
				return;*/

			Vector3 pos = (dir + transform.position) * range;

            if (Physics.Raycast(pos, -Vector3.up * pos.y, out RaycastHit hit, mlManager.GetFSMSettings().sightRange))
                pos = hit.point;

            if (NavMesh.SamplePosition(pos, out NavMeshHit navHit, GameConfig.wanderTargetRandomRadius, NavMesh.AllAreas))
                navAgent.SetDestination(navHit.position); // forward or not?
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
		public override void CollectObservations(VectorSensor sensor)
		{
			sensor.AddObservation(mlManager.AgentObservations.numOfVisibleEnemies);
			sensor.AddObservation(mlManager.AgentObservations.teamPerformance);
			sensor.AddObservation(mlManager.AgentObservations.targetTeamID);
			sensor.AddObservation(mlManager.AgentObservations.targetPosition.normalized);
            sensor.AddObservation(mlManager.AgentObservations.spottedEnemy.normalized);
        }
        public override void OnActionReceived(ActionBuffers actions)
		{
            ActionSegment<float> conActions = actions.ContinuousActions;

			Vector3 moveDir = new Vector3(conActions[0], conActions[1], conActions[2]);
			float range = conActions[3] * rangeMultiply;

			lastDir = moveDir; lastRange = range;

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
