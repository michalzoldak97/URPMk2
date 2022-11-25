namespace URPMk2
{
	public class ActiveDefenderStateManager : MLStateManager
    {
        public ActiveDefenderAgentObservations AgentObservations { get; set; }
        protected override void SetBehaviorReferences()
		{
            AgentObservations = new ActiveDefenderAgentObservations();
            exploreState = new ActiveDefenderExploreState(this);
            alertState = new ActiveDefenderAlertState(this);
            combatState = new ActiveDefenderCombatState(this);
        }
	}
}
