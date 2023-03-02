using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace URPMk2
{
	public class AISpawnerMashroom : MonoBehaviour, IAISpawner
	{
        [SerializeField] private AISpawnerSettingsSO spawnerSettings;
        [SerializeField] private Transform[] possibleSpawnPositions;

        private Vector3 currentSpawnPos;
        private List<GameObject> spawnedObjects = new List<GameObject>();

        private bool IsThresholdSatisfied()
        {

            for (int i = spawnedObjects.Count - 1; i >= 0; i--)
            {
                if (spawnedObjects[i] == null || !spawnedObjects[i].activeSelf)
                    spawnedObjects.RemoveAt(i);
            }

            if (spawnedObjects.Count > spawnerSettings.agentsThreshold)
                return false;

            return true;

        }
        private Vector3 SampleSpawnPosition()
        {
            Vector3 newSpawnPos = 
                possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length - 1)].position;
            int safetyLimiter = 0;
            while (newSpawnPos == currentSpawnPos &&
                safetyLimiter < 999)
            {
                newSpawnPos = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length - 1)].position;
                safetyLimiter++;
            }
            return newSpawnPos;
        }

        private IEnumerator SpawnSquad(AIWaypoints path)
        {
            currentSpawnPos = SampleSpawnPosition();
            foreach (AISquadType aType in spawnerSettings.squad)
            {
                for (int i = 0; i < aType.numToSpawn; i++)
                {
                    currentSpawnPos.x += Random.Range(-5f, 5f);
                    currentSpawnPos.z += Random.Range(-5f, 5f);
                    GameObject agent = Instantiate(aType.agent, currentSpawnPos, transform.rotation);
                    agent.GetComponent<ISpawnable>().SetWaypoints(path.waypoints);
                    spawnedObjects.Add(agent);
                    yield return new WaitForSeconds(
                        Random.Range(
                            spawnerSettings.singleSpawnFreqRange[0],
                            spawnerSettings.singleSpawnFreqRange[1]
                        )
                    );
                }
            }
        }
        private IEnumerator SpawnSquadPeriodic(AIWaypoints[] paths)
        {
            for (int i = 1; i < spawnerSettings.maxSquads; i++)
            {
                yield return new WaitForSeconds(spawnerSettings.squadSpawnPeriod);
                if (IsThresholdSatisfied())
                    StartCoroutine(SpawnSquad(paths[Random.Range(0, paths.Length)]));
            }
        }

        public void StartSpawnProcess(AIWaypoints[] paths, GeneralAgentSpawnManager spawnManager)
        {
            StartCoroutine(SpawnSquad(paths[Random.Range(0, paths.Length)]));
            StartCoroutine(SpawnSquadPeriodic(paths));
        }
    }
}
