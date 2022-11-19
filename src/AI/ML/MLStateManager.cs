using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace URPMk2
{
	public class MLStateManager : MonoBehaviour, IStateManager
    {
        [SerializeField] private FSMSettingsSO FSMSettings;
        public FSMSettingsSO GetFSMSettings() { return FSMSettings; }
        public bool IsInStruckState { get; private set; }
        public float GetCheckRate() { return checkRate; }
        public Transform AgentTransform { get; private set; }
        public Transform PursueTarget { get; set; }
        public NPCMaster MyNPCMaster { get; private set; }
        public NavMeshAgent MyNavMeshAgent { get; private set; }
        public MLAgentObservations AgentObservations { get; set; }
        public void SetWaypoints(Transform[] waypoints) { }

        public IMLState currentState;
        public IMLState capturedState;
        public MLExploreState exploreState;
        public MLAlertState alertState;
        public MLCombatState combatState;

        private bool isInformingAllies;
        private float checkRate, nextCheck;
        private WaitForSeconds waitForRecover, waitForNextAlliesInform;
        private DamagableMaster dmgMaster;
        private FSMRotationController rotationController;
        private IMLAgent mlAgent;

        private void SetStateReferences()
        {
            exploreState = new MLExploreState(this);
            alertState = new MLAlertState(this);
            combatState = new MLCombatState(this);
        }
        private void SetInit()
        {
            checkRate = Random.Range(
                FSMSettings.checkRate - FSMSettings.checkRateOffset,
                FSMSettings.checkRate + FSMSettings.checkRateOffset);
            AgentTransform = transform;
            MyNPCMaster = GetComponent<NPCMaster>();
            dmgMaster = GetComponent<DamagableMaster>();
            MyNavMeshAgent = GetComponent<NavMeshAgent>();
            rotationController = GetComponent<FSMRotationController>();
            waitForRecover = new WaitForSeconds(FSMSettings.recoverFromDmgTime);
            waitForNextAlliesInform = new WaitForSeconds(FSMSettings.informAlliesPeriod);
            AgentObservations = new MLAgentObservations();
            mlAgent = GetComponent<IMLAgent>();

            currentState = exploreState;
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
        private IEnumerator RecoverFromStruckState()
        {
            yield return waitForRecover;

            if (MyNavMeshAgent.enabled)
                MyNavMeshAgent.isStopped = false;
        }
        private void ActivateStruckState(Transform dummy, float dmg)
        {
            StopAllCoroutines();

            if (MyNavMeshAgent.enabled)
                MyNavMeshAgent.isStopped = true;

            StartCoroutine(RecoverFromStruckState());
        }
        private void RunUpdateActions()
        {
            float t = Time.time;
            if (t <= nextCheck)
                return;

            nextCheck = checkRate + t;
            currentState.UpdateState();

            mlAgent.PassReward();
        }
        private void Update()
        {
            RunUpdateActions();
        }
        private IEnumerator ResetInformState()
        {
            yield return waitForNextAlliesInform;
            isInformingAllies = false;
        }
        public void AlertAllies(Transform target)
        {
            if (!FSMSettings.shouldInformAllies ||
                isInformingAllies ||
                target == null)
                return;

            isInformingAllies = true;

            List<ITeamMember> teamMembersInRange = TeamMembersManager.GetTeamMembersInRange(FSMSettings.teamID, AgentTransform.position, FSMSettings.informAlliesRangePow);

            int numAllies = teamMembersInRange.Count;

            for (int i = 0; i < numAllies; i++)
            {
                teamMembersInRange[i].NMaster.CallEventAlertAboutEnemy(target);
            }

            StartCoroutine(ResetInformState());
        }
        public void RotateTowardsTarget()
        {
            rotationController.StartRotateTowardsTransform(PursueTarget);
        }
        public void LaunchWeaponSystem()
        {
            MyNPCMaster.CallEventAttackTarget(PursueTarget);
        }
    }
}
