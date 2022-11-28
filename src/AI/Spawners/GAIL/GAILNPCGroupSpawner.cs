using UnityEngine;

namespace URPMk2
{
	public class GAILNPCGroupSpawner : MonoBehaviour
	{
		[SerializeField] private bool isTarget;
		[SerializeField] private GameObject agentPrefab;
		[SerializeField] private Transform[] spawnPoints;
		[SerializeField] private GAILControlPanelManager gManager;

		private int agentsCount;
		private void SpawnNewGroup()
		{
			foreach (Transform sPoint in spawnPoints)
			{
				GameObject agent = Instantiate(agentPrefab, sPoint.position, sPoint.rotation);
				agent.GetComponent<GAILSpawnerAgentInstance>().SetGroupSpawner(this);
				agentsCount++;
			}
		}
		public void OnGroupAgentDestroyed()
		{
			agentsCount--;

			if (agentsCount > 0)
				return;

			SpawnNewGroup();

			if (!isTarget)
				return;

			gManager.OnEpisodeWon();
        }
		private void Start()
		{
			SpawnNewGroup();
        }
	}
}
