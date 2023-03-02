using UnityEngine;

namespace URPMk2
{
	public class GeneralAgentSpawnManager : MonoBehaviour 
	{
        [SerializeField] private Transform[] inceptorSpawners;
		[SerializeField] private AIWaypoints[] inceptorPaths;
        [SerializeField] private Transform[] convoSpawners;
        [SerializeField] private AIWaypoints[] convoPaths;

        public delegate void GeneralAgentSpawnManagerEventHandler();
        public event GeneralAgentSpawnManagerEventHandler EventSquadSpawned;

        public void CallEventSquadSpawned()
        {
            EventSquadSpawned?.Invoke();
        }
        private void Start()
        {
           /* QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;*/

            foreach (Transform iSpawner in inceptorSpawners)
            {
                iSpawner.GetComponent<IAISpawner>().StartSpawnProcess(inceptorPaths, this);
            }
            foreach (Transform cSpawner in convoSpawners)
            {
                cSpawner.GetComponent<IAISpawner>().StartSpawnProcess(convoPaths, this);
            }
        }
	}
}