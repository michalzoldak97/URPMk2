namespace URPMk2
{
    public class AISpawnerPeriodic : MonoBehaviour, IAISpawner
    {
        [SerializeField] private AISpawnerSettingsSO spawnerSettings;

        private int squadsSpawned;
        private GameObject[] squadMembers;

        private Vector3 SampleSpawnPosition()
        {
            bool foundCleanPos = false;
            int numAttempts = 0;
            Vector3 rndPoint = pos.position;
            rndPoint.x += Random.Range(-shiftRange, shiftRange);
            rndPoint.y += Random.Range(-shiftRange, shiftRange);

            while (!foundCleanPos &&
                numAttempts < 10)
            {
                if (Physics.Raycast(rndPoint, -Vector3.up * rndPoint.y, out RaycastHit hit, shiftRange + 1f))
                    rndPoint = hit.point;

                if (NavMesh.SamplePosition(rndPoint, out NavMeshHit navHit, shiftRange, NavMesh.AllAreas))
                {
                    MoveToCleanPos(navHit.position);
                    foundCleanPos = true;
                }
                rndPoint = Random.insideUnitSphere * 3f;
                numAttempts++;
            }
        }

        private void SpawnSquad(AIWaypoints path)
        {
            foreach (AISquadType aType in spawnerSettings.squad)
            {
                for (int i = 0; i < aType.numToSpawn; i++)
                {
                    
                }
            }
        }

        public void StartSpawnProcess(AIWaypoints[] paths)
        {
            SpawnSquad(paths[Random.Range(0, path.Length + 1)]);
        }
    }
}