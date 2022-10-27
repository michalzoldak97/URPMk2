namespace URPMk2
{
    public class AISpawnerPeriodic : MonoBehaviour, IAISpawner
    {
        [SerializeField] private AISpawnerSettingsSO spawnerSettings;

        private int squadsSpawned;

        private void SpawnSquad(AIWaypoints path)
        {

        }

        public void StartSpawnProcess(AIWaypoints[] paths)
        {
            SpawnSquad(paths[Random.Range(0, path.Length + 1)]);
        }
    }
}