using UnityEngine;

namespace URPMk2
{
	public class SquadDefenderCombatState : MLCombatState
    {
        private readonly SquadDefenderStateManager stateManager;
        public SquadDefenderCombatState(SquadDefenderStateManager mlManager)
        {
            this.mlManager = mlManager;
            this.stateManager = mlManager;
        }
        protected override void UpdateObservations()
        {
            stateManager.AgentObservations.AttackTarget =
                target != null ?
                target.ObjTransform.position :
                new Vector3(-1f, -1f, -1f);
        }
    }
}
