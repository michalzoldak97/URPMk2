
using UnityEngine;

namespace URPMk2
{
	/// <summary>
	/// Spawns no ML agents and/or targets
	/// Destroying all group member should end episode rewarding winner group id
	/// If no ML agents win TrainingUnitManager will handle this with non existing win group id
	/// </summary>
	public class TrainingNoAgentGroupSpawner : MonoBehaviour
	{
		[SerializeField] private bool isTargetGroup;
		[SerializeField] private int oppositeAgentTeamGroupID;
		[SerializeField] private int maxSquads;
		[SerializeField] private GameObject agentPrefab;
		[SerializeField] private Transform[] spawnPoints;
        [SerializeField] private AIWaypoints[] agentPaths;
		[SerializeField] private TrainingUnitManager tuManager;

		private int activeAgentsCount;
        private int squadsNum;
        private void OnEnable()
		{
            tuManager.EventStartNewEpisode += ResetSquadsNum;
            tuManager.EventStartNewEpisode += SpawnNewTeam;
        }
		
		private void OnDisable()
		{
            tuManager.EventStartNewEpisode -= ResetSquadsNum;
            tuManager.EventStartNewEpisode -= SpawnNewTeam;
        }
		public void OnAgentDestroyed(Transform killer)
		{
			activeAgentsCount--;

            if (activeAgentsCount < 1 &&
				squadsNum < maxSquads)
			{
				squadsNum++;
				SpawnNewTeam();
            }

            if (!isTargetGroup)
				return;

			tuManager.CallEventAddGroupReward(oppositeAgentTeamGroupID, 0.1f);

			if (activeAgentsCount < 1)
				tuManager.CallEventEndEpisode(oppositeAgentTeamGroupID, 2);
        }
		private void SpawnNewTeam()
		{
			activeAgentsCount = 0;

            bool shouldAssignWaypoints = agentPaths.Length > 0;
			foreach (Transform sPos in spawnPoints)
			{
				GameObject agent = Instantiate(agentPrefab, sPos.position, sPos.rotation);
				agent.GetComponent<NoAgentGroupInstance>().SetTrainingNoAgentGroupSpawner(this);
				activeAgentsCount++;

                if (!shouldAssignWaypoints)
					continue;

                AIWaypoints path = agentPaths[Random.Range(0, agentPaths.Length)];
                agent.GetComponent<IStateManager>().SetWaypoints(path.waypoints);
            }
		}
		private void ResetSquadsNum()
		{
			squadsNum = 1;
        }
	}
}
