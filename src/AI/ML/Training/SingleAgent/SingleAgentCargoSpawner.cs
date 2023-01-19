using UnityEngine;

namespace URPMk2
{
	public class SingleAgentCargoSpawner : MonoBehaviour
	{
        [SerializeField] private GameObject cargoPrefab;
        [SerializeField] private Transform[] spawnPositions;
        [SerializeField] private AIWaypoints[] allWaypoints;
        private SingleAgentTrainingManager saManager;
        private void SetInit()
        {
            saManager = GetComponent<SingleAgentTrainingManager>();
        }
        /*private void Start()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }*/
        private void OnEnable()
        {
            SetInit();
            saManager.EventStartEpisode += OnEpisodeStart;
            saManager.EventAgentDestroyed += OnAgentDestroyed;
            saManager.EventFinishEpisode += OnEpisodeFinish;
        }

        private void OnDisable()
        {
            saManager.EventStartEpisode -= OnEpisodeStart;
            saManager.EventAgentDestroyed -= OnAgentDestroyed;
            saManager.EventFinishEpisode -= OnEpisodeFinish;
        }
        private void CallAgentDestroyed(Transform o)
        {
            saManager.CallEventAgentDestroyed(1);
        }
        private void OnEpisodeStart()
        {
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                GameObject cargo = Instantiate(cargoPrefab, spawnPositions[i].position, spawnPositions[i].rotation);
                cargo.GetComponent<DamagableMaster>().EventDestroyObject += CallAgentDestroyed;
                cargo.GetComponent<CargoController>().SetAIWaypoints(allWaypoints);
            }
        }
        private void OnAgentDestroyed(int i)
        {
            int spawnID = Random.Range(0, spawnPositions.Length);
            GameObject cargo = Instantiate(cargoPrefab, spawnPositions[spawnID].position, spawnPositions[spawnID].rotation);
            cargo.GetComponent<DamagableMaster>().EventDestroyObject += CallAgentDestroyed;
            cargo.GetComponent<CargoController>().SetAIWaypoints(allWaypoints);
        }
        private void OnEpisodeFinish()
        {

        }
    }
}
