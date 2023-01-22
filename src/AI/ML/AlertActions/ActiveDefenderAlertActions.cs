using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class ActiveDefenderAlertActions : MonoBehaviour
	{
        private WaitForSeconds waitForAlertRestore;
        private ActiveDefenderStateManager mlManager;
        private NPCMaster npcMaster;
        private void SetInit()
        {
            mlManager = GetComponent<ActiveDefenderStateManager>();
            npcMaster = GetComponent<NPCMaster>();
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

            mlManager.AgentObservations.SpottedEnemyMapPosition = new Vector3(-1f, -1f, -1f);
        }
        private void OnAlertReceived(Transform target)
        {
            mlManager.AgentObservations.SpottedEnemyMapPosition = target.position;
            StopAllCoroutines();
            StartCoroutine(ClearSpottedEnemyInfo());
        }
    }
}
