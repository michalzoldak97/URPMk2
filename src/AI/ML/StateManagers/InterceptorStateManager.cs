namespace URPMk2
{
	public class InterceptorStateManager : MLStateManager
    {
		public InterceptorAgentObservations AgentObservations { get; set; }

        protected override void SetBehaviorReferences()
        {
            AgentObservations = new InterceptorAgentObservations();
            AgentObservations.SpottedEnemyMapPosition = new UnityEngine.Vector3(-1f, -1f, -1f);
            exploreState = new InterceptorExploreState(this);
            alertState = new InterceptorAlertState(this);
            combatState = new InterceptorCombatState(this);
        }
    }
}
