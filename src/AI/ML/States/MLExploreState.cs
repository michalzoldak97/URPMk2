namespace URPMk2
{
	public class MLExploreState : IMLState
	{
        protected MLStateManager mlManager;
        private void Look()
        {
            FSMTarget target = mlManager.MyNPCMaster.NpcLook.IsTargetVisible();

            if (!target.isVisible)
                return;

            mlManager.PursueTarget = target.targetTransform;
            mlManager.currentState = mlManager.alertState;
        }
        protected virtual void UpdateObservations() {}
        public void UpdateState()
		{
            Look();
        }
	}
}
