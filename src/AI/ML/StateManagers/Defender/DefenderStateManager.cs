namespace URPMk2
{
	public class DefenderStateManager : MLStateManager
    {
        public Transform CargoParent { get; private set; }
        public DefenderAgentObservations AgentObservations { get; set; }

        public void SetCargoParent(Transform cargoParent)
        {
            CargoParent = cargoParent;
        }

        protected override void SetBehaviorReferences()
		{
            AgentObservations = new DefenderAgentObservations();
            exploreState = new DefenderExploreState(this);
            alertState = new DefenderAlertState(this);
            combatState = new DefenderCombatState(this);
        }
	}
}
