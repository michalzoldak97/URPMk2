using UnityEngine;

namespace URPMk2
{
	public class FSMAllertActions : MonoBehaviour
	{
		private NPCMaster npcMaster;
		private FSMStateManager fManager;
		private void SetInit()
		{
			npcMaster = GetComponent<NPCMaster>();
			fManager = GetComponent<FSMStateManager>();
		}
		
		private void OnEnable()
		{
			SetInit();
			npcMaster.EventAlertAboutEnemy += OnAlertReceived;
		}
		
		private void OnDisable()
		{
			npcMaster.EventAlertAboutEnemy -= OnAlertReceived;
		}
		private void OnAlertReceived(Transform target)
        {
			if (fManager.currentState != fManager.patrolState)
				return;

			fManager.PursueTarget = target;
			fManager.LocationOfInterest = target.position;
			fManager.currentState = fManager.alertState;
		}
	}
}
