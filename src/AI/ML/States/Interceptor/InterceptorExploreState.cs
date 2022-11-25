using UnityEngine;

namespace URPMk2
{
	public class InterceptorExploreState : MLExploreState
	{
        private readonly InterceptorStateManager stateManager;
        public InterceptorExploreState(InterceptorStateManager mlManager)
        {
            this.mlManager = mlManager;
            this.stateManager = mlManager;
            UpdateObservations();
        }

        protected override void UpdateObservations()
        {
            stateManager.AgentObservations.EnemyMapPosition = new Vector3(-1f, -1f, -1f);
        }
    }
}
