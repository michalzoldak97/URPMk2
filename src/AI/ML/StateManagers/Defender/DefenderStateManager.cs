namespace URPMk2
{
	public class DefenderStateManager : MLStateManager
    {
        public DefenderAgentObservations AgentObservations { get; set; }
        protected override void SetBehaviorReferences()
		{
            AgentObservations = new DefenderAgentObservations();
            exploreState = new DefenderExploreState(this);
            alertState = new DefenderAlertState(this);
            combatState = new DefenderCombatState(this);
        }
	}
}
