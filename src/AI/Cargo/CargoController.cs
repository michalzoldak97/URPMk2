using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class CargoController : MonoBehaviour
	{
		[SerializeField] private CargoSettingsSO cargoSettings;
		[SerializeField] private Transform[] waypoints;
		public CargoSettingsSO GetCargoSettings { get { return cargoSettings; } }

		private int currentWaypoint;
		private float nextCheck;
		private NavMeshAgent navAgent;

		private void Start()
		{
			navAgent = GetComponent<NavMeshAgent>();
			navAgent.SetDestination(waypoints[0].position);
			currentWaypoint = 0;
        }
		private void MoveToWaypoints()
		{
			if (navAgent.pathPending ||
				navAgent.remainingDistance > navAgent.stoppingDistance)
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
