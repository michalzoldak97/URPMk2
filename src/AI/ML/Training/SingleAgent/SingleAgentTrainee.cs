using Unity.MLAgents;
using UnityEngine;

namespace URPMk2
{
	public class SingleAgentTrainee
	{
		public SingleAgentTrainee(int sIdx, GameObject agentObj, SingleAgentTrainingManager saManager)
		{
            spawnIdx = sIdx;
            AgentObj = agentObj;
            agent = agentObj.GetComponent<Agent>();
            dmgMaster = agentObj.GetComponent<DamagableMaster>();
            this.saManager = saManager;
            dmgMaster.EventDestroyObject += InformManager;
        }
        public GameObject AgentObj { get; private set; }
		private readonly int spawnIdx;
        private readonly Agent agent;
        private readonly DamagableMaster dmgMaster;
		private readonly SingleAgentTrainingManager saManager;

        public void RemoveAgent()
        {
            agent.EndEpisode();
        }
        private void InformManager(Transform killer)
        {
            saManager.CallEventAgentDestroyed(spawnIdx);
        }
	}
}
