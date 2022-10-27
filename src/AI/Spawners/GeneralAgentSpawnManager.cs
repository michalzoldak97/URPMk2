using UnityEngine;

namespace URPMk2
{
	public class GeneralAgentSpawnManager : MonoBehaviour 
	{
		[SerializeField] private AIWaypoints[] inceptorPaths;
        [SerializeField] private AIWaypoints[] convoPaths;

        [SerializeField] private Transform[] inceptorSpawners;
        [SerializeField] private Transform[] convoSpawners;
        
        private void Start()
        {
            foreach (Transform iSpawner in inceptorSpawners)
            {
                iSpawner.GetComponent<IAISpawner>().StartSpawnProcess(inceptorPaths);
            }
            foreach (Transform cSpawner in convoSpawners)
            {
                cSpawner.GetComponent<IAISpawner>().StartSpawnProcess(convoPaths);
            }
        }
	}
}