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
			tuManager.EventAgentDamaged += OnAgentDamaged;
			tuManager.EventAgentDestroyed += OnAgentDestroyed;
			tuManager.EventEndEpisode += OnEpisodeEnd;
        }
		
		private void OnDisable()
		{
            tuManager.EventAgentDamaged -= OnAgentDamaged;
            tuManager.EventAgentDestroyed -= OnAgentDestroyed;
            tuManager.EventEndEpisode += OnEpisodeEnd;
        }
		private void OnAgentDamaged(Transform origin, Transform damaged,float dmg)
		{
			IMultiAgentGroupMember agentToReward = tuManager.GetMultiAgentGroupMember(origin);
			if (agentToReward != null)
                agentToReward.Agent.AddReward(dmg * 0.01f); 

			IMultiAgentGroupMember agentToPunish = tuManager.GetMultiAgentGroupMember(damaged);
            if (agentToPunish != null)
                agentToPunish.Agent.AddReward(dmg * -0.002f); 
        }
		private void OnAgentDestroyed(Transform killer, Transform agent)
		{
			IMultiAgentGroupMember agentToReward = tuManager.GetMultiAgentGroupMember(killer);
			if (agentToReward == null)
				return;

			agentToReward.Agent.AddReward(0.25f);
			tuManager.CallEventAddGroupReward(agentToReward.GroupID, 0.25f);
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
            tuManager.CallEventAddGroupReward(winID, -1f);
        }
    }
}
