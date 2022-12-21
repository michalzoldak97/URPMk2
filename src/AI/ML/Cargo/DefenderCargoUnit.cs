using UnityEngine;

namespace URPMk2
{
	public class DefenderCargoUnit : MonoBehaviour, ICargoUnit
	{
		private float checkRate, nextCheck; 
		private Transform cargoParent;
		private DefenderAgentObservations agentObservations;

		private void OnCargoParentDamage(Transform o, float dmg)
		{
			agentObservations.CargoParentDamage = dmg;
		}

		private void OnCargoParentDestroy(Transform o)
		{
			SubscribeToCargoParentEvents(false);
			SearchCargoParent();
		}

		private void SubscribeToCargoParentEvents(bool shouldSubscribe)
		{
			if (cargoParent == null)
				return;
			
			DamagableMaster cargoParentDMGMaster = cargoParent.GetComponent<DamagableMaster>();
			if (shouldSubscribe)
			{
				cargoParentDMGMaster.EventReceivedDamage += OnCargoParentDamage;
				cargoParentDMGMaster.EventDestroyObject += OnCargoParentDestroy;
			}
			else
			{
				cargoParentDMGMaster.EventReceivedDamage -= OnCargoParentDamage;
				cargoParentDMGMaster.EventDestroyObject -= OnCargoParentDestroy;
			}
		}

		private void SearchCargoParent()
		{
			List<ITeamMember> possibleDefendTargets = 
			TeamMembersManager.GetTeamMembersInRange(
				fManager.GetFSMSettings().teamsToDefend, 
				fTransform.position, 
				250000);

			if (possibleDefendTargets.Count < 1)
            {
				agentObservations.CargoParentMapPosition = Vector3.zero;
				return;
			}

			int defendTargetIdx = Random.Range(0, possibleDefendTargets.Count);
			SetCargoParent(possibleDefendTargets[defendTargetIdx].ObjTransform);
		}

		public void SetCargoParent(Transform cargoParent)
		{
			this.cargoParent = cargoParent;
			SubscribeToCargoParentEvents(true);
		}

		private void Start()
		{
			dsManager = GetComponent<DefenderStateManager>();
			agentObservations = dsManager.AgentObservations;
			checkRate = dsManager.GetCheckRate();
		}

		private void UpdateCargoParentPosition()
		{
			if (cargoParent == null)
			{
				SearchCargoParent();
				return;
			}

			agentObservations.CargoParentMapPosition = cargoParent.position;
		}

		private void FixedUpdate()
		{
			if (Time.time < nextCheck)
				return;

			nextCheck = Time.time + checkRate;

			UpdateCargoParentPosition();
		}
	}
}
