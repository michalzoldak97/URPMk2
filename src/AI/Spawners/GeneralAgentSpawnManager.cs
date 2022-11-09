using UnityEngine;

namespace URPMk2
{
	public class GeneralAgentSpawnManager : MonoBehaviour 
	{
        [SerializeField] private Transform[] inceptorSpawners;
		[SerializeField] private AIWaypoints[] inceptorPaths;
        [SerializeField] private Transform[] convoSpawners;
        [SerializeField] private AIWaypoints[] convoPaths;
        
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