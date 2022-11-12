using UnityEngine;

namespace URPMk2
{
	public class MLAlertState : IMLState
	{
        private int enemyNotSeenCnt, enemyDetections;
        private readonly MLStateManager mlManager;
        public MLAlertState(MLStateManager mlManager)
        {
            this.mlManager = mlManager;
        }
        private void Look()
		{
            FSMTarget target = mlManager.MyNPCMaster.NpcLook.IsTargetVisible();

            if (!target.isVisible) 
            {
                enemyNotSeenCnt++;
                enemyDetections = 0;
                return;
            }

            enemyNotSeenCnt = 0;
            enemyDetections++;

            mlManager.AlertAllies(target.targetTransform);
        }
        private void DoAlertActions()
        {
            if (enemyNotSeenCnt >= mlManager.GetFSMSettings().targetLostDetections)
            {
                mlManager.PursueTarget = null;
                mlManager.currentState = mlManager.exploreState;
                return;
            }

            if (enemyDetections >= mlManager.GetFSMSettings().requiredDetectionCount)
                mlManager.currentState = mlManager.combatState;
        }
        private void UpdateObservations()
        {
            bool pursueTargetExists = mlManager.PursueTarget != null;

            mlManager.AgentObservations.numOfVisibleEnemies =
                pursueTargetExists ? (1 / mlManager.GetFSMSettings().enemiesBufferSize) : 0;
            mlManager.AgentObservations.distanceToEnemy =
                pursueTargetExists ?
                Vector3.Distance(mlManager.PursueTarget.position, mlManager.AgentTransform.position) / GameConfig.maxSceneDistance :
                0f;
            mlManager.AgentObservations.enemyDirection =
                pursueTargetExists ? 
                (mlManager.PursueTarget.position - mlManager.AgentTransform.position).normalized :
                Vector3.zero;
        }

        public void UpdateState()
		{
			Look();
            DoAlertActions();
            UpdateObservations();
		}
	}
}
