using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class CargoBoardingSpawner : MonoBehaviour
	{
        [SerializeField] Transform[] spawnPositions;

        private int botsSpawned;
        private WaitForSeconds waitForSpawnCheck;
        private GameObject[] spawnedAgents;
        private CargoController cargoController;

        private void SpawnBot(int posIdx)
        {
            GameObject bot = Instantiate(
                cargoController.CargoSettings.botObj, 
                spawnPositions[posIdx].position, 
                spawnPositions[posIdx].rotation);

            bot.GetComponent<IStateManager>().MyFollowTarget = gameObject.transform;
            spawnedAgents[botsSpawned] = bot;
        }
        private void SpawnBoarding(int remaining) 
        {
            int max = remaining > spawnPositions.Length ? spawnPositions.Length : remaining;
            for (int i = 0; i < max; i++)
            {
                SpawnBot(i);
                botsSpawned++;
            }
        }
        private bool IsBotsAliveThresholdExceeded()
        {
            int botsAlive = 0;

            for (int i = 0; i < spawnedAgents.Length; i++)
            {
                if (spawnedAgents[i] != null &&
                    spawnedAgents[i].activeSelf)
                    botsAlive++;
            }

            return botsAlive <= cargoController.CargoSettings.botsAliveThreshold;
        }
        private IEnumerator SpawnProcess()
        {
            int remaining = cargoController.CargoSettings.boardingCapacity - botsSpawned;
            while (remaining > 0)
            {
                if (IsBotsAliveThresholdExceeded())
                {
                    SpawnBoarding(remaining);
                    remaining = cargoController.CargoSettings.boardingCapacity - botsSpawned;
                }

                yield return waitForSpawnCheck;
            }
        }
        private void Start()
        {
            cargoController = GetComponent<CargoController>();
            waitForSpawnCheck = new WaitForSeconds(cargoController.CargoSettings.checkSpawnInterval);
            spawnedAgents = new GameObject[cargoController.CargoSettings.boardingCapacity];
            StartCoroutine(SpawnProcess());
        }
    }
}
