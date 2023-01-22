using UnityEngine;

namespace URPMk2
{
	public class SquadDefenderAlertState : MLAlertState
    {
        private readonly SquadDefenderStateManager stateManager;
        public SquadDefenderAlertState(SquadDefenderStateManager mlManager)
        {
            this.mlManager = mlManager;
            this.stateManager = mlManager;
        }
        protected override void UpdateObservations()
        {
            stateManager.AgentObservations.AttackTarget =
                mlManager.PursueTarget != null ?
                mlManager.PursueTarget.position :
                new Vector3(-1f, -1f, -1);
        }
    }
}
