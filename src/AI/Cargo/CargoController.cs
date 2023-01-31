using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class CargoController : MonoBehaviour, ISpawnable
	{
		[SerializeField] private CargoSettingsSO cargoSettings;
		private AIWaypoints[] allWaypoints;
		public void SetAIWaypoints(AIWaypoints[] toSet)
		{
			allWaypoints = toSet;
		}
		public void SetWaypoints(Transform[] toSet) 
		{
			waypoints = toSet;
		}
		public CargoSettingsSO CargoSettings { get { return cargoSettings; } }

		private int currentWaypoint;
		private float nextCheck;
		private Transform[] waypoints;
		private NavMeshAgent navAgent;

		private void SetWaypoints()
		{
			if (allWaypoints == null || 
				allWaypoints.Length < 0)
				return;

            int pathID = Random.Range(0, allWaypoints.Length);
            waypoints = allWaypoints[pathID].waypoints;
        }

		private void Start()
		{
			SetWaypoints();
            navAgent = GetComponent<NavMeshAgent>();
			navAgent.SetDestination(waypoints[0].position);
			currentWaypoint = 0;
        }
		private void MoveToWaypoints()
		{
            if ((navAgent.remainingDistance > 1f &&
				navAgent.remainingDistance > navAgent.stoppingDistance) ||
				navAgent.pathPending)
				return;

			if (currentWaypoint + 1 < waypoints.Length)
				currentWaypoint++;

            navAgent.SetDestination(waypoints[currentWaypoint].position);
            navAgent.isStopped = false;
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
