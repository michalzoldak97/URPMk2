using UnityEngine;

namespace URPMk2
{
	public class SingleAgentGAILTrainingAdapter : MonoBehaviour
	{
		[SerializeField] private GAILControlPanelManager gManager;
		private SingleAgentTrainingManager saManager;
		private void SetInit()
		{
			saManager = GetComponent<SingleAgentTrainingManager>();
			if (gManager != null)
				return;

			gManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GAILControlPanelManager>();
        }
		
		private void OnEnable()
		{
			SetInit();
			saManager.EventNewAgentSpawned += gManager.SetNewAgent;
		}
		
		private void OnDisable()
		{
            saManager.EventNewAgentSpawned -= gManager.SetNewAgent;
        }
	}
}
