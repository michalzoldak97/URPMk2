using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class DefenderAlertActions : MonoBehaviour
	{
        private WaitForSeconds waitForAlertRestore;
        private DefenderStateManager mlManager;
        private NPCMaster npcMaster;
        private void SetInit()
        {
            mlManager = GetComponent<DefenderStateManager>();
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
            int observationsCount = mlManager.AgentObservations.SpottedEnemyMapPositions.Length;
            for (int i = 0; i < observationsCount; i++)
            {
                mlManager.AgentObservations.SpottedEnemyMapPositions[i] = new Vector3(-1f, -1f, -1f);
            }
        }
        private void OnAlertReceived(Transform target)
        {
            int observationsCount = mlManager.AgentObservations.SpottedEnemyMapPositions.Length;
            Vector3 emptyObservation = new Vector3(-1f, -1f, -1f);
            for (int i = 0; i < observationsCount; i++)
            {
                if (mlManager.AgentObservations.SpottedEnemyMapPositions[i] != emptyObservation ||
                    mlManager.AgentObservations.SpottedEnemyMapPositions[i] == target.position)
                    continue;

                mlManager.AgentObservations.SpottedEnemyMapPositions[i] = target.position;
            }
            StopAllCoroutines();
            StartCoroutine(ClearSpottedEnemyInfo());
        }
    }
}
