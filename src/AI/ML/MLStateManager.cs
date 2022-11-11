using System;
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
        public MLCombatState combatState;

        private float checkRate, nextCheck;
        private WaitForSeconds waitForRecover;
        private DamagableMaster dmgMaster;

        private void SetStateReferences()
        {
            exploreState = new MLExploreState(this);
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
            waitForRecover = new WaitForSeconds(FSMSettings.recoverFromDmgTime);
            AgentObservations = new MLAgentObservations();

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
        private void ActivateStruckState(float dmg)
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
        }
        private void Update()
        {
            RunUpdateActions();
        }
        ///<summary> Gives agent an information about the whole team performance
        /// If team inflicted more damage than enemies which should be fought with - result is 1
        /// Othervise team damage divided by enemy damage is returned///</summary>
        public int GetTeamPerformance()
        {
            int teamDmg = 0;
            int enemyDmg = 0;
            foreach (DamageObjectData d in GlobalDamageMaster.dmgStatistics.Values)
            {
                if (d.objTeam == FSMSettings.teamID)
                {
                    teamDmg += (int)d.dmg;
                    continue;
                }

                if (Array.Exists(FSMSettings.teamsToAttack, teamID => teamID == d.objTeam))
                    enemyDmg += (int)d.dmg;
            }

            if (teamDmg == 0 || enemyDmg == 0)
                return 0;

            int teamPerformance = (teamDmg / enemyDmg);

            if (teamPerformance > 1)
                return 1;

            return teamPerformance;
        }
    }
}
