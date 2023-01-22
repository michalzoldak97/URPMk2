
namespace URPMk2
{
	public class SquadDefenderStateManager : MLStateManager
    {
        public SquadDefenderAgentObservations AgentObservations { get; private set; }

        protected override void SetBehaviorReferences()
        {
            AgentObservations = new SquadDefenderAgentObservations();
            exploreState = new SquadDefenderExploreState(this);
            alertState = new SquadDefenderAlertState(this);
            combatState = new SquadDefenderCombatState(this);
        }
    }
}
