using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class CargoController : MonoBehaviour
	{
		[SerializeField] private CargoSettingsSO cargoSettings;
		private AIWaypoints[] allWaypoints;
		public void SetAIWaypoints(AIWaypoints[] toSet)
		{
			allWaypoints = toSet;
		}
		public CargoSettingsSO GetCargoSettings { get { return cargoSettings; } }

		private int currentWaypoint;
		private float nextCheck;
		private Transform[] waypoints;
		private NavMeshAgent navAgent;

		private void Start()
		{
			int pathID = Random.Range(0, allWaypoints.Length);
			waypoints = allWaypoints[pathID].waypoints;
			navAgent = GetComponent<NavMeshAgent>();
			navAgent.SetDestination(waypoints[0].position);
			currentWaypoint = 0;
        }
		private void MoveToWaypoints()
		{
            if (navAgent.remainingDistance > 1f &&
				navAgent.remainingDistance > navAgent.stoppingDistance)
				return;

			if (currentWaypoint + 1 < waypoints.Length)
				currentWaypoint++;

            navAgent.isStopped = false;
            navAgent.SetDestination(waypoints[currentWaypoint].position);
		}
		private void Update()
		{
			if (Time.time <= nextCheck)
				return;

			nextCheck = cargoSettings.checkRate + Time.time;

			MoveToWaypoints();
        }
	}
}
