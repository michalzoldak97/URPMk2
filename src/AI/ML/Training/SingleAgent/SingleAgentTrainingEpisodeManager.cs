using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class SingleAgentTrainingEpisodeManager : MonoBehaviour
	{
		private SingleAgentTrainingManager saManager;
		private void SetInit()
		{
			saManager = GetComponent<SingleAgentTrainingManager>();
        }
		private void Start()
		{
            StartNewEpisode();
        }

		private void OnEnable()
		{
			SetInit();
			saManager.EventFinishEpisode += StartNewEpisode;
		}
		private void OnDisable()
		{
            saManager.EventFinishEpisode -= StartNewEpisode;
        }
		private IEnumerator TimeoutEpisode()
		{
			yield return new WaitForSeconds(999999999);
			Debug.Log("Episode timeout");
			saManager.CallEventFinishEpisode();
		}
		private void StartNewEpisode()
		{
			StopAllCoroutines();
			StartCoroutine(TimeoutEpisode());
            saManager.CallEventStartEpisode();
        }
	}
}
