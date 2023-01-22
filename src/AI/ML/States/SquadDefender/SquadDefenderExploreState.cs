using UnityEngine;

namespace URPMk2
{
	public class SquadDefenderExploreState : MLExploreState
    {
		private readonly SquadDefenderStateManager stateManager;
		
		public SquadDefenderExploreState(SquadDefenderStateManager mlManager)
		{
			this.mlManager = mlManager;
			this.stateManager = mlManager;
			UpdateObservations();
		}

        protected override void UpdateObservations()
        {
			stateManager.AgentObservations.AttackTarget = new Vector3(-1f, -1f, -1f);
        }
    }
}
