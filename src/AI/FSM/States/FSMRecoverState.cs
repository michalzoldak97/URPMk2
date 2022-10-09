using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class FSMRecoverState : IFSMState
    {
        private Transform fTransform;
        private FSMStateManager fManager;
        public FSMRecoverState(FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
        }
        private void Defend()
        {
            if (fManager.PursueTarget == null ||
                !fManager.MyNPCMaster.NpcLook.IsPursueTargetVisible(fManager.PursueTarget))
            {
                ITeamMember[] enemiesInRange = fManager.MyNPCMaster.NpcLook.GetEnemiesInRange();

                if (!(System.Array.Exists(enemiesInRange, el => el != null)))
                    return;

                float minDist = fManager.SightRangePow * 2;
                float distToEnemy;

                int eirLen = enemiesInRange.Length;
                for (int i = 0; i < eirLen; i++)
                {
                    if (enemiesInRange[i] == null)
                        continue;

                    distToEnemy = (enemiesInRange[i].ObjTransform.position - fTransform.position).sqrMagnitude;

                    if (distToEnemy < minDist)
                    {
                        minDist = distToEnemy;
                        fManager.PursueTarget = enemiesInRange[i].ObjTransform;
                    }
                }
            }
            fManager.RotateTowardsTarget();
            fManager.LaunchWeaponSystem();
        }
        private List<ITeamMember> MoveTargetsInRange(Teams[] teams)
        {
            return TeamMembersManager.GetTeamMembersInRange(
                    teams,
                    fTransform.position,
                    fManager.SightRangePow
                );
        }
        private void SetDestination()
        {
            fManager.MyNavMeshAgent.stoppingDistance = 0;
            if (fManager.RecoverTarget == null)
            {
                fManager.MyNavMeshAgent.SetDestination(
                    TeamMembersManager.GetFinalTeamDestination(
                        fManager.GetFSMSettings().teamID));

            }
            fManager.MyNavMeshAgent.isStopped = false;
        }
        private void MoveToRecover(Teams[] teams)
        {
            List<ITeamMember> recoversInRange = MoveTargetsInRange(teams);

            for (int i = 0; i < recoversInRange.Count; i++)
            {
                if (recoversInRange[i] != null)
                {
                    fManager.RecoverTarget = recoversInRange[i].ObjTransform;
                    break;
                }
            }
            SetDestination();
        }
        private void MoveToRecoverTarget()
        {
            if (fManager.IsHealthLow)
                MoveToRecover(fManager.GetFSMSettings().healthRecoverTeams);
            else if (fManager.IsAmmoFinished)
                MoveToRecover(fManager.GetFSMSettings().ammoRecoverTeams);
            else
            {
                fManager.RecoverTarget = null;
                fManager.MyNavMeshAgent.stoppingDistance = fManager.GetFSMSettings().nmaStoppingDistance;
                fManager.SwitchState(false, fManager.patrolState);
            }
        }
        public void UpdateState()
        {
            MoveToRecoverTarget();
            Defend();
        }
    }
}