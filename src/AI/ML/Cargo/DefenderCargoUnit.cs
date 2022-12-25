using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

namespace URPMk2
{
	public class DefenderCargoUnit : MonoBehaviour, ICargoUnit
	{
		private float checkRate, nextCheck; 
		private Transform cargoParent, finalDest;
		private DefenderAgentObservations agentObservations;
		private DamagableMaster dmgMaster;
		DamagableMaster cargoParentDMGMaster;


        private void OnCargoParentDamage(Transform o, float dmg)
		{
            agentObservations.CargoParentDamage = dmg;
		}

		private void OnCargoParentDestroy(Transform o)
		{
			/*SubscribeToCargoParentEvents(false);
			SearchCargoParent();*/
			// training
			float distTofinal = Vector3.Distance(cargoParent.position, finalDest.position);
			float reward = distTofinal < 10 ? 1f : -1f;
			GetComponent<Agent>().AddReward(reward);
			GetComponent<Agent>().EndEpisode();
			Destroy(gameObject, GameConfig.secToDestroy);
            dmgMaster.CallEventDestroyObject(transform);
			gameObject.SetActive(false);
		}

		private void SubscribeToCargoParentEvents(bool shouldSubscribe)
		{
			if (cargoParent == null ||
				!cargoParent.gameObject.activeSelf)
				return;
			
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
		private void OnDisable()
		{
            cargoParentDMGMaster.EventReceivedDamage -= OnCargoParentDamage;
            cargoParentDMGMaster.EventDestroyObject -= OnCargoParentDestroy;
        }

		private void SearchCargoParent()
		{
            DefenderStateManager dsManager = GetComponent<DefenderStateManager>();
            List<ITeamMember> possibleDefendTargets = 
			TeamMembersManager.GetTeamMembersInRange(
                dsManager.GetFSMSettings().teamsToDefend,
                dsManager.AgentTransform.position, 
				250000);

			if (possibleDefendTargets.Count < 1)
            {
				agentObservations.CargoParentMapPosition = Vector3.zero;
				return;
			}

			int defendTargetIdx = Random.Range(0, possibleDefendTargets.Count);
			SetCargoParent(possibleDefendTargets[defendTargetIdx].ObjTransform, finalDest);
		}

		public void SetCargoParent(Transform cargoParent, Transform finalDest)
		{
			this.cargoParent = cargoParent;
			this.finalDest = finalDest;
			cargoParentDMGMaster = cargoParent.GetComponent<DamagableMaster>();

            SubscribeToCargoParentEvents(true);
		}

		private void Start()
		{
			dmgMaster = GetComponent<DamagableMaster>();
            DefenderStateManager dsManager = GetComponent<DefenderStateManager>();
			agentObservations = dsManager.AgentObservations;
			checkRate = dsManager.GetCheckRate();
		}

		private void UpdateCargoParentPosition()
		{
			if (cargoParent == null)
			{
				// SearchCargoParent();
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
