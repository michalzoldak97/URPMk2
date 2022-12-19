using UnityEngine;

namespace URPMk2
{
	public class DefenderAlertState : MLAlertState
    {
        private readonly DefenderStateManager stateManager;
        public DefenderAlertState(DefenderStateManager mlManager)
        {
            this.mlManager = mlManager;
            this.stateManager = mlManager;
        }

        protected override void UpdateObservations()
        {
            bool pursueTargetExists = mlManager.PursueTarget != null;

            stateManager.AgentObservations.NumOfVisibleEnemies =
                pursueTargetExists ? (1 / mlManager.GetFSMSettings().enemiesBufferSize) : 0;

            stateManager.AgentObservations.EnemyMapPosition =
                pursueTargetExists ?
                mlManager.PursueTarget.position :
                new Vector3(-1f, -1f, -1f);
        }
    }
}
