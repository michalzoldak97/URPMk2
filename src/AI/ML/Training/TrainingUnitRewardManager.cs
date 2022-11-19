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
        }
		
		private void OnDisable()
		{
            tuManager.EventAgentDamaged -= OnAgentDamaged;
            tuManager.EventAgentDestroyed -= OnAgentDestroyed;
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

			agentToReward.Agent.AddReward(1f);
			tuManager.CallEventAddGroupReward(agentToReward.GroupID, 0.25f);
		}
    }
}
