using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class FSMStateManager : MonoBehaviour, IStateManager
    {
		[SerializeField] protected FSMSettingsSO FSMSettings;
		public FSMSettingsSO GetFSMSettings() { return FSMSettings; }
		public bool IsHealthLow { get; private set; }
		public bool IsAmmoFinished { get; private set; }
		public int SightRangePow { get; protected set; }
		public float GetCheckRate() { return checkRate; }
		public Vector3 LocationOfInterest { get; set; }
		public Vector3 WanderTarget { get; set; }
		public Transform MyFollowTarget { get; set; }
		public Transform PursueTarget { get; set; }
		public Transform RecoverTarget { get; set; }
		public NavMeshAgent MyNavMeshAgent { get; protected set; }
		public NPCMaster MyNPCMaster { get; protected set; }
		public void SetWaypoints(Transform[] waypoints) 
		{
			this.waypoints = waypoints;
		}

		public Transform[] waypoints;
		public IFSMState currentState;
		public IFSMState capturedState;
		public FSMPatrolState patrolState;
		public FSMAlertState alertState;
		public FSMPursueState pursueState;
		public FSMAttackState attackState;
		public FSMFleeState fleeState;
		public FSMStruckState struckState;
		public FSMInvestigateDangerState investigateDangerState;
		public FSMFollowState followState;
		public FSMRecoverState recoverState;

		private bool isInformingAllies;
		protected float checkRate, nextCheck;
		protected Transform myTransform;
		protected WaitForSeconds waitForRecover, waitForNextAlliesInform;
		protected DamagableMaster dmgMaster;
		protected NPCRotationController rotationController;
		protected virtual void SetStateReferences()
		{
			patrolState = new FSMPatrolState(this);
			alertState = new FSMAlertState(this);
			pursueState = new FSMPursueState(this);
			fleeState = new FSMFleeState(this);
			followState = new FSMFollowState(this);
			attackState = new FSMAttackState(this);
			struckState = new FSMStruckState(this);
			recoverState = new FSMRecoverState(this);
		}

		protected virtual void SetInit()
		{
			MyNPCMaster = GetComponent<NPCMaster>();
			dmgMaster = GetComponent<DamagableMaster>();
			MyNavMeshAgent = GetComponent<NavMeshAgent>();
			checkRate = Random.Range(
				FSMSettings.checkRate - FSMSettings.checkRateOffset,
				FSMSettings.checkRate + FSMSettings.checkRateOffset);
			waitForRecover = new WaitForSeconds(FSMSettings.recoverFromDmgTime);
			waitForNextAlliesInform = new WaitForSeconds(FSMSettings.informAlliesPeriod);
            SightRangePow = FSMSettings.sightRange * FSMSettings.sightRange;
			myTransform = transform;
			rotationController = GetComponent<FSMRotationController>();
			currentState = patrolState;
		}

		private void OnEnable()
		{
			SetStateReferences();
			SetInit();
			dmgMaster.EventReceivedDamage += ActivateStruckState;
			dmgMaster.EventHealthLow += ActivateHealthRecoverState;
			dmgMaster.EventHealthRecovered += StopHealthRecoverState;
			MyNPCMaster.EventAmmoFinished += ActivateAmmoRecoverState;
			MyNPCMaster.EventAmmoRecovered += StopAmmoRecoverState;
		}

		private void OnDisable()
		{
			dmgMaster.EventReceivedDamage -= ActivateStruckState;
			dmgMaster.EventHealthLow -= ActivateHealthRecoverState;
			dmgMaster.EventHealthRecovered -= StopHealthRecoverState;
			MyNPCMaster.EventAmmoFinished -= ActivateAmmoRecoverState;
			MyNPCMaster.EventAmmoRecovered -= StopAmmoRecoverState;
			StopAllCoroutines();
		}
		private void RunUpdateActions()
        {
			float t = Time.time;
			if (t > nextCheck)
            {
				nextCheck = checkRate + t;
				currentState.UpdateState();
            }
        }

		private IEnumerator RecoverFromStruckState()
		{
			yield return waitForRecover;

			if (MyNavMeshAgent.enabled)
				MyNavMeshAgent.isStopped = false;

			currentState = capturedState;
		}
		private void ActivateStruckState(Transform dummy, float dmg)
        {
			StopAllCoroutines();

			if (currentState != struckState)
				capturedState = currentState;

			if (MyNavMeshAgent.enabled)
				MyNavMeshAgent.isStopped = true;

			currentState = struckState;

			StartCoroutine(RecoverFromStruckState());
        }
		private void StopHealthRecoverState()
        {
			IsHealthLow = false;
		}
		private void ActivateHealthRecoverState()
        {
			IsHealthLow = true;
			currentState = recoverState;
        }
		private void StopAmmoRecoverState()
		{
			IsAmmoFinished = false;
		}
		private void ActivateAmmoRecoverState()
		{
			IsAmmoFinished = true;
			currentState = recoverState;
		}
		public void SwitchState(bool stopNavMeshAgent, IFSMState toState)
        {
			MyNavMeshAgent.isStopped = stopNavMeshAgent;
			currentState = toState;
        }
		private IEnumerator ResetInformState()
        {
			yield return waitForNextAlliesInform;

            isInformingAllies = false;
		}
		public void AlertAllies()
        {
			if (!FSMSettings.shouldInformAllies ||
				isInformingAllies ||
				PursueTarget == null)
				return;

			isInformingAllies = true;

			List<ITeamMember> teamMembersInRange = TeamMembersManager.GetTeamMembersInRange(FSMSettings.teamID, myTransform.position, FSMSettings.informAlliesRangePow);

			int numAllies = teamMembersInRange.Count;

			for (int i = 0; i < numAllies; i++)
            {
				teamMembersInRange[i].NMaster.CallEventAlertAboutEnemy(PursueTarget);
            }

			StartCoroutine(ResetInformState());
		}
		public bool CheckIfFollowDestinationReached()
        {
			if (MyNavMeshAgent.remainingDistance <= MyNavMeshAgent.stoppingDistance &&
                !MyNavMeshAgent.pathPending)
            {
				MyNavMeshAgent.isStopped = true;
				return true;
            }

			MyNavMeshAgent.isStopped = false;
			return false;
		}
		public void RotateTowardsTarget()
        {
			rotationController.StartRotateTowardsTransform(PursueTarget);
		}
		public void LaunchWeaponSystem()
        {
			MyNPCMaster.CallEventAttackTarget(PursueTarget);
        }
		private void Update()
        {
			RunUpdateActions();
		}
    }
}
