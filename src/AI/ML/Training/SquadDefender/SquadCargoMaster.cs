using Unity.MLAgents;
using UnityEngine;
using UnityEngine.SceneManagement;
using URPMk2;

namespace SD
{
	public class SquadCargoMaster : MonoBehaviour
	{
		public delegate void SquadCargoEventsHandler(float dmg);
		public event SquadCargoEventsHandler EventCargoDamaged;
        public event SquadCargoEventsHandler EventCargoDestroyed;

		private bool targetReached;
        private SimpleMultiAgentGroup agentGroup = new SimpleMultiAgentGroup();
		
		public void RegisterAgent(Agent agent)
		{
            agentGroup.RegisterAgent(agent);
        }
		public void OnTargetReached()
		{
			targetReached = true;
			float reward = GetComponent<DamagableMaster>().GetHealth() / 1200f;
            agentGroup.AddGroupReward(reward);
			agentGroup.EndGroupEpisode();
		}
		public void OnCargoDestroyed()
		{
			if (targetReached)
				return;

            agentGroup.AddGroupReward(-1f);
            agentGroup.EndGroupEpisode();
        }
		public void CallEventCargoDamaged(float dmg)
		{
			EventCargoDamaged?.Invoke(dmg);
        }
		public void CallEventCargoDestroyed(float dmg)
		{
            EventCargoDestroyed?.Invoke(dmg);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
