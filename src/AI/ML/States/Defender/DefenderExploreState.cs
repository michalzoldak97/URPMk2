using UnityEngine;

namespace URPMk2
{
    public class DefenderExploreState : MLExploreState
    {
        private readonly DefenderStateManager stateManager;
        public DefenderExploreState(DefenderStateManager mlManager)
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
