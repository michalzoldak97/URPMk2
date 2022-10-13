using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class DefenderFSMStateManager : FSMStateManager
	{
		public FSMDynamicDefendState dynamicDefendState;
		protected override void SetStateReferences()
		{
			patrolState = new FSMPatrolState(this);
			alertState = new FSMAlertState(this);
			pursueState = new FSMPursueState(this);
			fleeState = new FSMFleeState(this);
			followState = new FSMFollowState(this);
			attackState = new FSMAttackState(this);
			struckState = new FSMStruckState(this);
			recoverState = new FSMRecoverState(this);
			dynamicDefendState = new FSMDynamicDefendState(this);
		}
		protected override void SetInit()
		{
			MyNPCMaster = GetComponent<NPCMaster>();
			dmgMaster = GetComponent<DamagableMaster>();
			MyNavMeshAgent = GetComponent<NavMeshAgent>(); 
			checkRate = Random.Range(
				 FSMSettings.checkRate - FSMSettings.checkRateOffset,
				 FSMSettings.checkRate + FSMSettings.checkRateOffset);
			waitForRecover = new WaitForSeconds(FSMSettings.recoverFromDmgTime);
			SightRangePow = FSMSettings.sightRange * FSMSettings.sightRange;
			myTransform = transform;
			rotationController = GetComponent<FSMRotationController>();
			currentState = dynamicDefendState;
		}
	}
}
