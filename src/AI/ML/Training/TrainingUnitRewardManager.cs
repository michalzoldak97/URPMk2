using UnityEngine;

namespace URPMk2
{
	public class TrainingUnitRewardManager : MonoBehaviour
	{
		private TrainingUnitManager tuManager;
		private void SetInit()
		{
			tuManager = GetComponent<TrainingUnitManager>();
        }
		
		private void OnEnable()
		{
			SetInit();
			tuManager.EventAgentDestroyed += OnAgentDestroyed;
			tuManager.EventEndEpisode += OnEpisodeEnd;
        }
		
		private void OnDisable()
		{
            tuManager.EventAgentDestroyed -= OnAgentDestroyed;
            tuManager.EventEndEpisode += OnEpisodeEnd;
        }
		private void OnAgentDestroyed(Transform killer, Transform agent)
		{
			IMultiAgentGroupMember agentToReward = tuManager.GetMultiAgentGroupMember(killer);
			if (agentToReward == null)
				return;

			agentToReward.Agent.AddReward(0.25f);
			tuManager.CallEventAddGroupReward(agentToReward.GroupID, 0.01f);
		}
		private void OnEpisodeEnd(int winID, int looseID)
		{
			foreach(IMultiAgentGroupMember agent in tuManager.GetMultiAgentGroupMembers())
			{
				if (agent.GroupID == winID)
					agent.Agent.AddReward(1f);

				if (agent.GroupID == looseID)
					agent.Agent.AddReward(-1f);
			}

            tuManager.CallEventAddGroupReward(winID, 1f);
            tuManager.CallEventAddGroupReward(looseID, -1f);
        }
    }
}
