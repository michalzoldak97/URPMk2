namespace URPMk2
{
	public class MLAlertState : IMLState
	{

        protected MLStateManager mlManager;
        private int enemyNotSeenCnt, enemyDetections;
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

        protected virtual void UpdateObservations() { }
 

        public void UpdateState()
		{
			Look();
            UpdateObservations();
		}
	}
}
