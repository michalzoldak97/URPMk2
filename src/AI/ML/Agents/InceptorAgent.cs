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
		private float lastDmgInflicted, lastDmgReceived;
		private NavMeshAgent navAgent;
		private InceptorRewardCalculator rewardCalculator;

		private void Awake()
		{
			navAgent = GetComponent<NavMeshAgent>();
			rewardCalculator = new InceptorRewardCalculator(transform);
		}
		public override void OnEpisodeBegin()
		{
			base.OnEpisodeBegin();
		}
		public override void CollectObservations(VectorSensor sensor)
		{

		}
        public override void OnActionReceived(ActionBuffers actions)
		{

		}
		public void PassReward()
		{
			if (!isTrainingMode)
				return;

			float[] rewardPenalty = rewardCalculator.GetReward();
		}
    }
}
