using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class SquadDefenderAlertActions : MonoBehaviour
	{
        private WaitForSeconds waitForAlertRestore;
        private SquadDefenderStateManager mlManager;
        private NPCMaster npcMaster;
        private void SetInit()
        {
            mlManager = GetComponent<SquadDefenderStateManager>();
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

            mlManager.AgentObservations.SpottedTarget = new Vector3(-1f, -1f, -1f);
        }
        private void OnAlertReceived(Transform target)
        {
            mlManager.AgentObservations.SpottedTarget = target.position;
            StopAllCoroutines();
            StartCoroutine(ClearSpottedEnemyInfo());
        }
    }
}
