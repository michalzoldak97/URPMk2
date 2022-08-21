using UnityEngine;

namespace URPMk2
{
	public class FSMAttackState : IFSMState
    {
        // TO DO: npc weapon controller should avoid friendly fire
        private readonly float attackRangePow;
        private readonly Transform fTransform;
        private readonly INPCWeaponController weaponController;
        private readonly FSMStateManager fManager;
        public FSMAttackState(FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
            attackRangePow = fManager.GetFSMSettings().attackRange *
                fManager.GetFSMSettings().attackRange;
            weaponController = fManager.GetComponent<INPCWeaponController>();
        }
        private void AttemptAttack()
        {
            if (fManager.PursueTarget == null)
            {
                fManager.SwitchState(false, fManager.patrolState);
                return;
            }

            ITeamMember[] enemiesInRange = fManager.GetEnemiesInRange();
            if (!(System.Array.Exists(enemiesInRange, el => el != null))
                || (fManager.PursueTarget.position - fTransform.position).sqrMagnitude >
               attackRangePow) // see no enemies but target alive or target too far
            {
                Debug.Log("Getting back to the pursue state");
                fManager.currentState = fManager.pursueState;
                return;
            }

            if (!System.Array.Exists(enemiesInRange,
                el => el != null && el.ObjTransform == fManager.PursueTarget)) // see only other enemies
            {
                Debug.Log("Getting back to the patrol state because only other enemies found");
                fManager.PursueTarget = null;
                fManager.SwitchState(false, fManager.patrolState);
                return;
            }

            // weaponController.LaunchAtack();
            Debug.Log("Ratatatatatatatatatat");
            fManager.RotateTowardsTarget();

        }
        public void UpdateState()
        {
            AttemptAttack();
        }
    }
}
