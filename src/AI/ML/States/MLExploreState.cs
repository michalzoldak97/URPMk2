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

            mlManager.AgentObservations.numOfVisibleEnemies = 0;
            mlManager.AgentObservations.targetTeamID = -1;
            mlManager.AgentObservations.targetPosition = Vector3.zero;
            isFirstUpdate = false;
        }
        public void UpdateState()
		{
            Look();
            UpdateObservations();
        }
	}
}
