using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class AISpawnerContinous : MonoBehaviour, IAISpawner
    {
        // subscribe to created agents
        [SerializeField] private AISpawnerSettingsSO spawnerSettings;

        private int activeAgentsCount, spawnedSquadsCount;
        private AIWaypoints[] paths;

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

        private void MonitorSpawnProcess(Transform t)
        {
            activeAgentsCount--;

            if (activeAgentsCount > spawnerSettings.agentsThreshold ||
                spawnedSquadsCount >= spawnerSettings.maxSquads)
                return;

            SpawnSquad(paths[Random.Range(0, paths.Length)]);
        }

        private void SpawnSquad(AIWaypoints path)
        {
            foreach (AISquadType aType in spawnerSettings.squad)
            {
                for (int i = 0; i < aType.numToSpawn; i++)
                {
                    GameObject agent = Instantiate(aType.agent, SampleSpawnPosition(), transform.rotation);
                    agent.GetComponent<ISpawnable>().SetWaypoints(path.waypoints);
                    agent.GetComponent<DamagableMaster>().EventDestroyObject += MonitorSpawnProcess;
                    activeAgentsCount++;
                }
            }
            spawnedSquadsCount++;
        }
        public void StartSpawnProcess(AIWaypoints[] paths)
        {
            this.paths = paths;
            SpawnSquad(paths[Random.Range(0, paths.Length)]);
        }
    }
}
