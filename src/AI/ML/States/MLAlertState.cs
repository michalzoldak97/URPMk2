using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

                if (enemyNotSeenCnt >= mlManager.GetFSMSettings().targetLostDetections)
                {
                    mlManager.PursueTarget = null;
                    mlManager.currentState = mlManager.exploreState;
                }

                return;
            }

            enemyNotSeenCnt = 0;
            enemyDetections++;

            mlManager.AlertAllies(target.targetTransform);

            if (enemyDetections >= mlManager.GetFSMSettings().requiredDetectionCount)
            {
                mlManager.PursueTarget = target.targetTransform;
                mlManager.currentState = mlManager.combatState;
            }
        }

        private void UpdateObservations()
        {
            bool pursueTargetExists = mlManager.PursueTarget != null;

            mlManager.AgentObservations.numOfVisibleEnemies =
                pursueTargetExists ? (1 / mlManager.GetFSMSettings().enemiesBufferSize) : 0;
            mlManager.AgentObservations.distanceToEnemy =
                pursueTargetExists ?
                Vector3.Distance(mlManager.PursueTarget.position, mlManager.AgentTransform.position) / mlManager.GetFSMSettings().sightRange :
                -1f;
            mlManager.AgentObservations.enemyDirection =
                pursueTargetExists ? 
                (mlManager.PursueTarget.position - mlManager.AgentTransform.position).normalized :
                new Vector3(-1f, -1f, -1f);
        }

        public void UpdateState()
		{
			Look();
            UpdateObservations();
		}
	}
}
