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
