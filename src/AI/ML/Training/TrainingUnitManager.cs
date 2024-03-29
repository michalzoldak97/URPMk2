using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class TrainingUnitManager : MonoBehaviour
	{
		public delegate void TrainingUnitEventHandler();
		public event TrainingUnitEventHandler EventStartNewEpisode;

        public delegate void TrainingUnitAgentDestructionHandler(Transform killer, Transform agent);
        public event TrainingUnitAgentDestructionHandler EventAgentDestroyed;

        public delegate void TrainingUnitRewardEventManager(int groupID, float reward);
        public event TrainingUnitRewardEventManager EventAddGroupReward;

        public delegate void TrainingUnitTaskFinishedEventManager(int winID, int looseID);
        public event TrainingUnitTaskFinishedEventManager EventEndEpisode;

        public List<IMultiAgentGroupMember> GetMultiAgentGroupMembers() { return trainingUnitAgents; }
        private readonly List<IMultiAgentGroupMember> trainingUnitAgents = new List<IMultiAgentGroupMember>();
		private readonly Dictionary<Transform, IMultiAgentGroupMember> agents = new Dictionary<Transform, IMultiAgentGroupMember>();
        public IMultiAgentGroupMember GetMultiAgentGroupMember(Transform agent)
        {
            if (!agents.ContainsKey(agent))
                return null;

            return agents[agent];
        }

        private void Start()
		{
			CallEventStartNewEpisode();
        }

        public void RegisterAgent(IMultiAgentGroupMember agent)
        {
            if (trainingUnitAgents.Contains(agent) ||
                agents.ContainsKey(agent.AgentTransform))
                return;

            trainingUnitAgents.Add(agent);
            agents.Add(agent.AgentTransform, agent);
        }
        private void UnRegisterAgent(IMultiAgentGroupMember agent)
        {
            if (!trainingUnitAgents.Contains(agent) ||
               !agents.ContainsKey(agent.AgentTransform))
                return;

            trainingUnitAgents.Remove(agent);
            agents.Remove(agent.AgentTransform);
        }
        private bool IsEpisodeFinished(int groupID)
        {
            for (int i = 0; i < trainingUnitAgents.Count; i++)
            {
                if (trainingUnitAgents[i].GroupID == groupID)
                    return false;
            }
            return true;
        }
		public void CallEventStartNewEpisode()
		{
			EventStartNewEpisode?.Invoke();
        }
        public void CallEventAgentDestroyed(Transform killer, Transform agent)
        {
            EventAgentDestroyed?.Invoke(killer, agent);

            int agentGroupID = agents[agent].GroupID;
            UnRegisterAgent(agents[agent]);

            if (!IsEpisodeFinished(agentGroupID))
                return;

            int winGroupID = agents.ContainsKey(killer) ? agents[killer].GroupID : 2; // TODO: handle Player
            CallEventEndEpisode(winGroupID, agentGroupID);
        }
        public void CallEventAddGroupReward(int groupID, float reward)
        {
            EventAddGroupReward?.Invoke(groupID, reward);
        }
        private IEnumerator ResetEpisode()
        {
            yield return new WaitForSeconds(9000);
            CallEventEndEpisode(-1, -1);
        }
        private void StartNewEpisode()
        {
            CallEventStartNewEpisode();
            StartCoroutine(ResetEpisode());
        }
        private void ClearAgentsList()
        {
            foreach (IMultiAgentGroupMember agent in trainingUnitAgents)
            {
                agent.AgentTransform.gameObject.SetActive(false);
                Destroy(agent.AgentTransform.gameObject, GameConfig.secToDestroy);
            }
            trainingUnitAgents.Clear();
            agents.Clear();
        }
        public void CallEventEndEpisode(int winID, int looseID)
        {
            EventEndEpisode?.Invoke(winID, looseID);
			ClearAgentsList();
            StopAllCoroutines();
			StartNewEpisode();
        }
    }
}
