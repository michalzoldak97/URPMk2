using UnityEngine;

namespace URPMk2
{
	public class InterceptorAlertState : MLAlertState
	{
        private readonly InterceptorStateManager stateManager;
        public InterceptorAlertState(InterceptorStateManager mlManager)
        {
            this.mlManager = mlManager;
            this.stateManager = mlManager;
        }

        protected override void UpdateObservations()
        {
            stateManager.AgentObservations.EnemyMapPosition =
                mlManager.PursueTarget != null ?
                mlManager.PursueTarget.position :
                new Vector3(-1f, -1f, -1f);
        }
    }
}
