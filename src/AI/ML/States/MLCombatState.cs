using System;
using UnityEngine;

namespace URPMk2
{
	public class MLCombatState : IMLState
	{
        protected float numOfEnemies;
        protected ITeamMember target;
        protected MLStateManager mlManager;
        private void Look()
		{
			bool shouldAssignTarget = target?.ObjTransform == null;

            numOfEnemies = 0;

            ITeamMember[] enemiesInRange  = mlManager.MyNPCMaster.NpcLook.GetEnemiesInRange();

            if (enemiesInRange[0] == null)
			{
                target = null;
				mlManager.PursueTarget = null;
				mlManager.currentState = mlManager.exploreState;
				return;
			}

			if (enemiesInRange[0].ObjTransform != mlManager.PursueTarget)
			{
                if (!Array.Exists(enemiesInRange, enemy => enemy != null && 
				enemy.ObjTransform == mlManager.PursueTarget))
                {
                    target = enemiesInRange[0];
                    mlManager.PursueTarget = target.ObjTransform;
                }
            }

            for (int i = 0; i < enemiesInRange.Length; i++)
			{
				if (enemiesInRange[i] != null)
					numOfEnemies++;
				else
					continue;

				if (shouldAssignTarget &&
					enemiesInRange[i].ObjTransform == mlManager.PursueTarget)
				{
                    target = enemiesInRange[i];
					shouldAssignTarget = false;
                }
            }

            mlManager.RotateTowardsTarget();
            mlManager.LaunchWeaponSystem();
            mlManager.AlertAllies(target.ObjTransform);
        }
		protected virtual void UpdateObservations() {}
        public void UpdateState()
		{
			Look();
			UpdateObservations();
        }
	}
}
