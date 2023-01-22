using Unity.MLAgents;
using UnityEngine;

namespace SD
{
	public class SquadCargoMaster : MonoBehaviour
	{
		public delegate void SquadCargoEventsHandler(int dmg);
		public event SquadCargoEventsHandler EventCargoDamaged;
        public event SquadCargoEventsHandler EventCargoOnTarget;
        public event SquadCargoEventsHandler EventCargoDestroyed;

        private SimpleMultiAgentGroup agentGroup = new SimpleMultiAgentGroup();
		
		public void RegisterAgent(Agent agent)
		{
            agentGroup.RegisterAgent(agent);
        }
		public void OnTargetReached()
		{
			agentGroup.AddGroupReward(1f);
			agentGroup.EndGroupEpisode();
		}
		public void OnCargoDestroyed()
		{
            agentGroup.AddGroupReward(-1f);
            agentGroup.EndGroupEpisode();
        }
		public void CallEventCargoDamaged(int dmg)
		{
			EventCargoDamaged?.Invoke(dmg);
        }
		public void CallEventCargoOnTarget(int dmg)
		{
			OnTargetReached();
            EventCargoOnTarget?.Invoke(dmg);
        }
		public void CallEventCargoDestroyed(int dmg)
		{
			OnCargoDestroyed();
            EventCargoDestroyed?.Invoke(dmg);
		}
    }
}
