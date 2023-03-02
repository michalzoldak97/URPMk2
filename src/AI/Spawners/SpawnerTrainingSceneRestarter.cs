using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace URPMk2
{
	public class SpawnerTrainingSceneRestarter : MonoBehaviour
	{
		[SerializeField] private int squadsToRestart = 250;

		private GeneralAgentSpawnManager spawnManager;
		private void SetInit()
		{
			spawnManager = GetComponent<GeneralAgentSpawnManager>();
        }
		
		private void OnEnable()
		{
			SetInit();
			// spawnManager.EventSquadSpawned += CheckIfRestartRequired;

        }
		
		private void OnDisable()
		{
            // spawnManager.EventSquadSpawned -= CheckIfRestartRequired;
        }
		private void CheckIfRestartRequired()
		{
			squadsToRestart--;
			if (squadsToRestart <= 0)
			{
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				return;
            }
        }

		private IEnumerator RestartSceneCount()
		{
			yield return new WaitForSeconds(1200);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Start()
        {
			StartCoroutine(RestartSceneCount());
        }
    }
}
