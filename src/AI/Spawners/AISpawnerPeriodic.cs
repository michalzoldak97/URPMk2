using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace URPMk2
{
    public class AISpawnerPeriodic : MonoBehaviour, IAISpawner
    {
        [SerializeField] private AISpawnerSettingsSO spawnerSettings;

        private Vector3 SampleSpawnPosition()
        {
            int numAttempts = 0;
            float spawnRadius = spawnerSettings.spawnRadius;
            Vector3 rndPoint = transform.position + Utils.GetVector3FromFloat(spawnerSettings.spawnPointOffset);

            rndPoint.x += Random.Range(0f, spawnRadius);
            rndPoint.y += Random.Range(0f, spawnRadius);

            while (numAttempts < 50)
            {
                if (Physics.Raycast(rndPoint, -Vector3.up * rndPoint.y, out RaycastHit hit, spawnRadius))
                    rndPoint = hit.point;

                if (NavMesh.SamplePosition(rndPoint, out NavMeshHit _, 1f, NavMesh.AllAreas))
                    return rndPoint;
                rndPoint += Random.insideUnitSphere * Random.Range(-3f, 3f);
                numAttempts++;
            }
            return transform.position;
        }

        private IEnumerator SpawnSquad(AIWaypoints path)
        {
            foreach (AISquadType aType in spawnerSettings.squad)
            {
                for (int i = 0; i < aType.numToSpawn; i++)
                {
                    GameObject agent = Instantiate(aType.agent, SampleSpawnPosition(), transform.rotation);
                    agent.GetComponent<IStateManager>().SetWaypoints(path.waypoints);
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
                StartCoroutine(SpawnSquad(paths[Random.Range(0, paths.Length)]));
            }
        }
        public void StartSpawnProcess(AIWaypoints[] paths)
        {
            StartCoroutine(SpawnSquad(paths[Random.Range(0, paths.Length)]));
            StartCoroutine(SpawnSquadPeriodic(paths));
        }
    }
}