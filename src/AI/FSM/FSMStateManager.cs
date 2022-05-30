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
		public Vector3 LocationOfInterest { get; private set; }
		public Vector3 WanderTarget { get; private set; }
		public Transform MyFollowTarget { get; private set; }
		public Transform PursueTarget { get; private set; }
		public Transform MyAttacker { get; private set; }
		public NavMeshAgent MyNavMeshAgent { get; private set; }
		public FSMMaster FSMMaster { get; private set; }

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

		private float checkRate;
		private WaitForSeconds waitForRecover; 

		private void SetInit()
		{
			FSMMaster = GetComponent<FSMMaster>();
			MyNavMeshAgent = GetComponent<NavMeshAgent>();
			checkRate = Random.Range(
				FSMSettings.checkRate - 0.15f, FSMSettings.checkRate + 0.15f);
			waitForRecover = new WaitForSeconds(FSMSettings.recoverFromDmgTime);
		}
		private void SetStateReferences()
        {

        }

		private void OnEnable()
		{
			SetInit();

		}

		private void OnDisable()
		{

		}
		private void UpdateStates()
        {

        }
		private void ActivatePatrolState()
        {

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

        }
		private void ActivateStruckState(int dummy)
        {

        }
		private void ActivateInvestigateDangerState()
        {

        }
		private void ActivateFollowState()
        {

        }
		private IEnumerator RecoverFromStruckState()
        {
			yield return waitForRecover;
        }
		public void OnEnemyAttack()
        {

        }
		public void SetMyAttacker(Transform attacker)
        {

        }
		public void Distract(Vector3 distractionPos)
        {

        }
        private void Update()
        {
            
        }
    }
}
