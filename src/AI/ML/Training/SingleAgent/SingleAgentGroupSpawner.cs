using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class SingleAgentGroupSpawner : MonoBehaviour
	{
		[SerializeField] GameObject agentPrefab;
		[SerializeField] Transform[] spawnPositions;

		private SingleAgentTrainingManager saManager;
		private readonly List<SingleAgentTrainee> agents = new List<SingleAgentTrainee>();
		private void SetInit()
		{
			saManager = GetComponent<SingleAgentTrainingManager>();
        }
		
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
        private void OnEpisodeStart()
        {
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                GameObject agent = Instantiate(
                    agentPrefab,
                    spawnPositions[i].position,
                    spawnPositions[i].rotation);

                agents.Add(
                    new SingleAgentTrainee(
                        i,
                        agent,
                        saManager)
                    );
            }
        }
        private void OnAgentDestroyed(int idx)
		{
			agents[idx].RemoveAgent();
            GameObject agent = Instantiate(
                    agentPrefab,
                    spawnPositions[idx].position,
                    spawnPositions[idx].rotation);
			agents[idx] = new SingleAgentTrainee(
						idx,
						agent,
						saManager);
        }
		private void OnEpisodeFinish()
		{
			for (int i = 0; i < spawnPositions.Length; i++)
			{
				agents[i].RemoveAgent();
            }
			agents.Clear();
        }
	}
}
