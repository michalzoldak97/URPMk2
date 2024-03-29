using UnityEngine;

namespace URPMk2
{
	public class FSMAttackState : IFSMState
    {
        // TO DO: npc weapon controller should avoid friendly fire
        private readonly float attackRangePow;
        private readonly Transform fTransform;
        private readonly FSMStateManager fManager;
        public FSMAttackState(FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
            attackRangePow = fManager.GetFSMSettings().attackRange *
                fManager.GetFSMSettings().attackRange;
        }
        private void AttemptAttack()
        {
            if (fManager.PursueTarget == null)
            {
                fManager.SwitchState(false, fManager.patrolState);
                return;
            }
            else if (fManager.MyNPCMaster.NpcLook.IsPursueTargetVisible(fManager.PursueTarget))
            {
                fManager.RotateTowardsTarget();
                fManager.LaunchWeaponSystem();
                return;
            }

            ITeamMember[] enemiesInRange = fManager.MyNPCMaster.NpcLook.GetEnemiesInRange();
            if (!(System.Array.Exists(enemiesInRange, el => el != null))
                || (fManager.PursueTarget.position - fTransform.position).sqrMagnitude >
               attackRangePow) // see no enemies but target alive or target too far
            {
                fManager.currentState = fManager.pursueState;
                return;
            }

            if (!System.Array.Exists(enemiesInRange,
                el => el != null && el.ObjTransform == fManager.PursueTarget)) // see only other enemies
            {
                fManager.PursueTarget = null;
                fManager.SwitchState(false, fManager.patrolState);
                return;
            }

            fManager.RotateTowardsTarget();
            fManager.LaunchWeaponSystem();
        }
        public void UpdateState()
        {
            AttemptAttack();
        }
    }
}
