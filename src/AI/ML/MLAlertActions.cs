using System.Collections;
using UnityEngine;
namespace URPMk2
{
	public class MLAlertActions : MonoBehaviour
	{
		private WaitForSeconds waitForAlertRestore;
		private MLStateManager mlManager;
        private NPCMaster npcMaster;
        private void SetInit()
		{
			mlManager = GetComponent<MLStateManager>();
			npcMaster = mlManager.MyNPCMaster;
			waitForAlertRestore = new WaitForSeconds(mlManager.GetFSMSettings().informAlliesPeriod);
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
		private IEnumerator ClearSpottedEnemyInfo()
		{
			yield return waitForAlertRestore;

			mlManager.AgentObservations.spottedEnemy = Vector3.zero;
        }
        private void OnAlertReceived(Transform target)
		{
			mlManager.AgentObservations.spottedEnemy = target.position;
			StopAllCoroutines();
			StartCoroutine(ClearSpottedEnemyInfo());
		}
    }
}
