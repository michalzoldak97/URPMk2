using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using URPMk2;

namespace SD
{
	public class SquadCargoSpawner : MonoBehaviour
	{
        [SerializeField] Transform[] spawnPositions;

        private int botsSpawned;
        private WaitForSeconds waitForSpawnCheck;
        private Dictionary<GameObject, Agent> spawnedAgents;
        private CargoController cargoController;

        private SquadCargoMaster cargoMaster;
		private void SetInit()
		{
            cargoMaster = GetComponent<SquadCargoMaster>();
        }
        private void SpawnAgent(int posIdx)
        {
            GameObject bot = Instantiate(
                cargoController.CargoSettings.botObj,
                spawnPositions[posIdx].position,
                spawnPositions[posIdx].rotation);

            bot.GetComponent<IStateManager>().MyFollowTarget = gameObject.transform;
            spawnedAgents.Add(bot, bot.GetComponent<Agent>());
            cargoMaster.RegisterAgent(bot.GetComponent<Agent>());
        }
        private void SpawnBoarding(int remaining)
        {
            int max = remaining > spawnPositions.Length ? spawnPositions.Length : remaining;
            for (int i = 0; i < max; i++)
            {
                SpawnAgent(i);
                botsSpawned++;
            }
        }
        private bool IsBotsAliveThresholdExceeded()
        {
            int botsAlive = 0;

            foreach (KeyValuePair<GameObject, Agent> agent in spawnedAgents)
            {
                if (agent.Key != null &&
                    agent.Key.activeSelf)
                {
                    botsAlive++;
                }
            }

            return botsAlive <= cargoController.CargoSettings.botsAliveThreshold;
        }
        private IEnumerator SpawnProcess()
        {
            int remaining = cargoController.CargoSettings.boardingCapacity - botsSpawned;
            while (remaining > 0)
            {
                if (IsBotsAliveThresholdExceeded())
                {
                    SpawnBoarding(remaining);
                    remaining = cargoController.CargoSettings.boardingCapacity - botsSpawned;
                }

                yield return waitForSpawnCheck;
            }
        }
        private void Start()
        {
            cargoController = GetComponent<CargoController>();
            waitForSpawnCheck = new WaitForSeconds(cargoController.CargoSettings.checkSpawnInterval);
            spawnedAgents = new Dictionary<GameObject, Agent>();
            StartCoroutine(SpawnProcess());
        }
        private void OnEnable()
		{
			SetInit();
            cargoMaster.EventCargoDamaged += OnCargoDamaged;
            cargoMaster.EventCargoOnTarget += OnEpisodeEnd;
            cargoMaster.EventCargoDestroyed += OnEpisodeEnd;
		}
		
		private void OnDisable()
		{
            cargoMaster.EventCargoDamaged -= OnCargoDamaged;
            cargoMaster.EventCargoOnTarget -= OnEpisodeEnd;
            cargoMaster.EventCargoDestroyed -= OnEpisodeEnd;
        }
        private void OnCargoDamaged(float dmg)
        {
            foreach (KeyValuePair<GameObject, Agent> agent in spawnedAgents)
            {
                if (agent.Key != null &&
                    agent.Key.activeSelf)
                {
                    agent.Value.AddReward(dmg * -0.001f);
                }
            }
        }
        private void OnEpisodeEnd(float _)
        {
            foreach (KeyValuePair<GameObject, Agent> agent in spawnedAgents)
            {
                Destroy(agent.Key, GameConfig.secToDestroy);
                agent.Key.SetActive(false);
            }
        }
	}
}
