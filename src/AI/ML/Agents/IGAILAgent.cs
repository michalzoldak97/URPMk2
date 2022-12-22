using UnityEngine;

namespace URPMk2
{
	public interface IGAILAgent
	{
		public void SetGAILManager(GAILControlPanelManager gManager);
		public void OnAgentWon();
		public void SetHDestination(Vector3 toSetPos);

    }
}
