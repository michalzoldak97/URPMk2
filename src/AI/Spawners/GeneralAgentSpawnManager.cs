using UnityEngine;

namespace URPMk2
{
	public class GeneralAgentSpawnManager : MonoBehaviour 
	{
		[SerializeField] private AIWaypoints[] inceptorPaths;
        [SerializeField] private AIWaypoints[] convoPaths;

        [SerializeField] private IAISpawner[] inceptorSpawners;
        [SerializeField] private IAISpawner[] convoSpawners;
        
        private void Start()
        {
            foreach (IAISpawner iSpawner in inceptorSpawners)
            {
                iSpawner.StartSpawnProcess(inceptorPaths);
            }
            foreach (IAISpawner cSpawner in convoSpawners)
            {
                iSpawner.StartSpawnProcess(convoPaths);
            }
        }
	}
}