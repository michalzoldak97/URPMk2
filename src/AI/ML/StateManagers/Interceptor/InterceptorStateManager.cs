namespace URPMk2
{
	public class InterceptorStateManager : MLStateManager
    {
		public InterceptorAgentObservations AgentObservations { get; set; }

        protected override void SetBehaviorReferences()
        {
            AgentObservations = new InterceptorAgentObservations();
            exploreState = new InterceptorExploreState(this);
            alertState = new InterceptorAlertState(this);
            combatState = new InterceptorCombatState(this);
        }
    }
}
