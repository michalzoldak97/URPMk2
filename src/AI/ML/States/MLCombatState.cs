using UnityEngine;

namespace URPMk2
{
	public class MLCombatState : IMLState
	{
		private int numOfEnemies;
		private ITeamMember target;
        private readonly MLStateManager mlManager;
		public MLCombatState(MLStateManager mlManager)
		{
			this.mlManager = mlManager;
        }
		private void Look()
		{
            numOfEnemies = 0;

            ITeamMember[] enemiesInRange  = mlManager.MyNPCMaster.NpcLook.GetEnemiesInRange();
            
			if (enemiesInRange[0] == null)
			{
				Debug.Log("First is null");
                target = null;
				mlManager.PursueTarget = null;
				mlManager.currentState = mlManager.exploreState;
				return;
			}

			target = enemiesInRange[0];
			mlManager.PursueTarget = target.ObjTransform;

			for (int i = 1; i < enemiesInRange.Length; i++)
			{
				if (enemiesInRange[i] != null)
					numOfEnemies++;
			}
			numOfEnemies += 1;
        }
		private void UpdateObservations()
		{
			mlManager.AgentObservations.numOfVisibleEnemies = numOfEnemies > 0 ?
				(numOfEnemies / mlManager.GetFSMSettings().enemiesBufferSize) : 0;

			bool targetExists = target != null;

			mlManager.AgentObservations.targetTeamID = targetExists ?
				(int)target.TeamID : -1;

			mlManager.AgentObservations.targetPosition = targetExists ?
				target.ObjTransform.position : Vector3.zero;
        }
        public void UpdateState()
		{
			Debug.Log("Im in combat state");
			Look();
			UpdateObservations();
        }
	}
}
