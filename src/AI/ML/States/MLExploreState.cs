using UnityEngine;
namespace URPMk2
{
	public class MLExploreState : IMLState
	{
        private bool isFirstUpdate = true;
        private readonly MLStateManager mlManager;
        public MLExploreState(MLStateManager mlManager)
        {
            this.mlManager = mlManager;
        }
        private void Look()
        {
            FSMTarget target = mlManager.MyNPCMaster.NpcLook.IsTargetVisible();

            if (!target.isVisible)
                return;

            mlManager.PursueTarget = target.targetTransform;
            mlManager.currentState = mlManager.alertState;
        }
        private void UpdateObservations()
        {
            if (!isFirstUpdate)
                return;

            mlManager.AgentObservations.numOfVisibleEnemies = 0f;
            mlManager.AgentObservations.distanceToEnemy = -1f;
            mlManager.AgentObservations.enemyDirection = new Vector3(-1f, -1f, -1f);
            isFirstUpdate = false;
        }
        public void UpdateState()
		{
            Look();
            UpdateObservations();
        }
	}
}
