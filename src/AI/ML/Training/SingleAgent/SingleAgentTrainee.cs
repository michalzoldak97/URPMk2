using Unity.MLAgents;
using UnityEngine;

namespace URPMk2
{
	public class SingleAgentTrainee
	{
		public SingleAgentTrainee(int sIdx, GameObject agentObj, SingleAgentTrainingManager saManager)
		{
            spawnIdx = sIdx;
            agent = agentObj.GetComponent<Agent>();
            dmgMaster = agentObj.GetComponent<DamagableMaster>();
            this.saManager = saManager;
            dmgInfo = new DamageInfo(DamageType.Gun, saManager.transform);
            dmgInfo.dmg = 999;
            dmgMaster.EventDestroyObject += InformManager;
        }
		private readonly int spawnIdx;
        private readonly Agent agent;
        private readonly DamagableMaster dmgMaster;
		private readonly SingleAgentTrainingManager saManager;
        private readonly DamageInfo dmgInfo;

        public void RemoveAgent()
        {
            agent.EndEpisode();
            dmgMaster.CallEventHitByGun(dmgInfo);
        }
        private void InformManager(Transform killer)
        {
            saManager.CallEventAgentDestroyed(spawnIdx);
        }
	}
}
