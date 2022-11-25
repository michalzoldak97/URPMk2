using UnityEngine;

namespace URPMk2
{
    public class ActiveDefenderExploreState : MLExploreState
    {
        private readonly ActiveDefenderStateManager stateManager;
        public ActiveDefenderExploreState(ActiveDefenderStateManager mlManager)
        {
            this.mlManager = mlManager;
            this.stateManager = mlManager;
            UpdateObservations();
        }

        protected override void UpdateObservations()
        {
            stateManager.AgentObservations.NumOfVisibleEnemies = 0f;
            stateManager.AgentObservations.EnemyMapPosition = new Vector3(-1f, -1f, -1f);
        }
    }
}
