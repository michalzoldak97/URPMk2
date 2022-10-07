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
		public int SightRangePow { get; private set; }
		public float GetCheckRate() { return checkRate; }
		public Vector3 LocationOfInterest { get; set; }
		public Vector3 WanderTarget { get; set; }
		public Transform MyFollowTarget { get; private set; }
		public Transform PursueTarget { get; set; }
		public Transform MyAttacker { get; private set; }
		public NavMeshAgent MyNavMeshAgent { get; private set; }
		public NPCMaster MyNPCMaster { get; private set; }
		public VisibilityParamContainer VisibilityParams { get; private set; }


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

		private bool isInformingAllies;
		private float checkRate, nextCheck;
		private Vector3 heading;
		private Transform myTransform;
		private WaitForSeconds waitForRecover;
		private DamagableMaster dmgMaster;
		private FSMTarget targetNotFound;
		private ITeamMember[] enemiesBuffer;
		private NPCRotationController rotationController;

		private void SetInit()
		{
			MyNPCMaster = GetComponent<NPCMaster>();
			dmgMaster = GetComponent<DamagableMaster>();
			MyNavMeshAgent = GetComponent<NavMeshAgent>();
			checkRate = Random.Range(
				FSMSettings.checkRate - FSMSettings.checkRateOffset, FSMSettings.checkRate + FSMSettings.checkRateOffset);
			waitForRecover = new WaitForSeconds(FSMSettings.recoverFromDmgTime);
			VisibilityParams = new VisibilityParamContainer(
				FSMSettings.sightRange,
				FSMSettings.highResDetectionRange,
				FSMSettings.highResDetectionRange * FSMSettings.highResDetectionRange, 
				FSMSettings.sightLayers, 
				head);
			SightRangePow = FSMSettings.sightRange * FSMSettings.sightRange;
			myTransform = transform;
			targetNotFound = new FSMTarget(false, null);
			enemiesBuffer = new ITeamMember[FSMSettings.enemiesBufferSize];
			rotationController = GetComponent<FSMRotationController>();
			currentState = patrolState;
		}
		private void SetStateReferences()
        {
			patrolState = new FSMPatrolState(this);
			alertState = new FSMAlertState(this);
			pursueState = new FSMPursueState(this);
			fleeState = new FSMFleeState(this);
			followState = new FSMFollowState(this);
			attackState = new FSMAttackState(this);
			struckState = new FSMStruckState(this);
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
		public void SwitchState(bool stopNavMeshAgent, IFSMState toState)
        {
			MyNavMeshAgent.isStopped = stopNavMeshAgent;
			currentState = toState;
        }
		private float CalculateDotProd(Transform target)
		{
			heading = (target.position - myTransform.position).normalized;
			return Vector3.Dot(heading, myTransform.forward);
		}
		public FSMTarget IsTargetVisible()
        {
			List<ITeamMember> enemiesInRange = TeamMembersManager.GetTeamMembersInRange(
					FSMSettings.teamsToAttack,
					myTransform.position,
					SightRangePow
				);

			int numEnemies = enemiesInRange.Count;
			for (int i = 0; i < numEnemies; i++)
			{
				bool isInHighResRange = heading.sqrMagnitude < VisibilityParams.highResSearchSqrRange;
				if (CalculateDotProd(enemiesInRange[i].ObjTransform) < FSMSettings.minDotProd &&
					!isInHighResRange)
					continue;

				if (VisibilityCalculator.IsVisibleSingle(VisibilityParams, heading, enemiesInRange[i].ObjTransform)
					|| (isInHighResRange &&
					VisibilityCalculator.IsVisibleCorners(VisibilityParams, enemiesInRange[i].ObjTransform, enemiesInRange[i].BoundsExtens)))
					return new FSMTarget(true, enemiesInRange[i].ObjTransform);
			}
			return targetNotFound;
		}

		public ITeamMember[] GetEnemiesInRange()
        {
			System.Array.Clear(enemiesBuffer, 0, enemiesBuffer.Length);

			List<ITeamMember> enemiesInRange = TeamMembersManager.GetTeamMembersInRange(
					FSMSettings.teamsToAttack,
					myTransform.position,
					SightRangePow
				);

			Teams topTeam = FSMSettings.teamID;

			int enemiesAdded = 0;
			int numEnemies = enemiesInRange.Count;
			for (int i = 0; i < numEnemies; i++)
			{
				if (enemiesAdded >= enemiesBuffer.Length)
					break;

				bool isInHighResRange = heading.sqrMagnitude < VisibilityParams.highResSearchSqrRange;

				if (CalculateDotProd(enemiesInRange[i].ObjTransform) < FSMSettings.minDotProd &&
					!isInHighResRange)
                    continue;

				if (VisibilityCalculator.IsVisibleSingle(VisibilityParams, heading, enemiesInRange[i].ObjTransform)
					|| (isInHighResRange &&
					VisibilityCalculator.IsVisibleCorners(VisibilityParams, enemiesInRange[i].ObjTransform, enemiesInRange[i].BoundsExtens)))
				{
					if (topTeam == FSMSettings.teamID)
						topTeam = enemiesInRange[i].TeamID;
					else if (enemiesInRange[i].TeamID != topTeam)
						break;

					enemiesBuffer[enemiesAdded] = enemiesInRange[i];
					enemiesAdded++;
				}
			}
			return enemiesBuffer;
		}

		private async void ResetInformState()
        {
			await System.TimeSpan.FromSeconds(FSMSettings.informAlliesPeriod);
			isInformingAllies = false;
		}
		public void AlertAllies()
        {
			if (!FSMSettings.shouldInformAllies ||
				isInformingAllies)
				return;

			isInformingAllies = true;

			List<ITeamMember> teamMembersInRange = TeamMembersManager.GetTeamMembersInRange(FSMSettings.teamID, myTransform.position, FSMSettings.informAlliesRangePow);

			int numAllies = teamMembersInRange.Count;

			for (int i = 0; i < numAllies; i++)
            {
				teamMembersInRange[i].NMaster.CallEventAlertAboutEnemy(PursueTarget);
            }

			ResetInformState();
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
			rotationController.RotateTowardsTransform(PursueTarget);
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
