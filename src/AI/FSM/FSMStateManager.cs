using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class FSMStateManager : MonoBehaviour
	{
		[SerializeField] private Transform head;
		[SerializeField] private FSMSettingsSO FSMSettings;
		public FSMSettingsSO GetFSMSettings() { return FSMSettings; }
		public Vector3 LocationOfInterest { get; set; }
		public Vector3 WanderTarget { get; private set; }
		public Transform MyFollowTarget { get; private set; }
		public Transform PursueTarget { get; private set; }
		public Transform MyAttacker { get; private set; }
		public NavMeshAgent MyNavMeshAgent { get; private set; }
		public FSMMaster FSMMaster { get; private set; }
		public VisibilityParamContainer VisibilityParams { get; private set; }

		public Transform[] waypoints;
		public IComparer priorityComparer;
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

		private float checkRate, nextCheck;
		private WaitForSeconds waitForRecover;
		private DamagableMaster dmgMaster;

		private void SetInit()
		{
			FSMMaster = GetComponent<FSMMaster>();
			dmgMaster = GetComponent<DamagableMaster>();
			MyNavMeshAgent = GetComponent<NavMeshAgent>();
			checkRate = Random.Range(
				FSMSettings.checkRate - 0.15f, FSMSettings.checkRate + 0.15f);
			waitForRecover = new WaitForSeconds(FSMSettings.recoverFromDmgTime);
			VisibilityParams = new VisibilityParamContainer(
				FSMSettings.sightRange,
				FSMSettings.highResDetectionRange,
				FSMSettings.highResDetectionRange * FSMSettings.highResDetectionRange, 
				FSMSettings.sightLayers, 
				head);
			ActivatePatrolState();
		}
		private void SetStateReferences()
        {
			patrolState = new FSMPatrolState(this);
        }

		private void OnEnable()
		{
			SetStateReferences();
			SetInit();
			dmgMaster.EventReceivedDamage += ActivateStruckState;
		}

		private void OnDisable()
		{
			dmgMaster.EventReceivedDamage -= ActivateStruckState;
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
		private void ActivatePatrolState()
        {
			currentState = patrolState;
        }
		private void ActivateAlertState()
        {

        }
		private void ActivatePursueState()
        {

        }
		private void ActivateAttackState()
        {

        }
		private void ActivateFleeState()
        {
			if (currentState == struckState)
            {
				capturedState = fleeState;
				return;
            }
			currentState = fleeState;
        }
		private IEnumerator RecoverFromStruckState()
		{
			yield return waitForRecover;

			if (MyNavMeshAgent.enabled)
				MyNavMeshAgent.isStopped = false;

			currentState = capturedState;
		}
		private void ActivateStruckState(float dmg)
        {
			StopAllCoroutines();

			if (currentState != struckState)
				capturedState = currentState;

			if (MyNavMeshAgent.enabled)
				MyNavMeshAgent.isStopped = true;

			currentState = struckState;

			StartCoroutine(RecoverFromStruckState());
        }
		private void ActivateInvestigateDangerState()
        {

        }
		private void ActivateFollowState()
        {

        }
		public void OnEnemyAttack()
        {

        }
		public void SetMyAttacker(Transform attacker)
        {
			MyAttacker = attacker;
        }
		public void Distract(Vector3 distractionPos)
        {
			LocationOfInterest = distractionPos;

			if (currentState == patrolState)
				currentState = alertState;
        }
        private void Update()
        {
			RunUpdateActions();
		}
    }
}
