using UnityEngine;

namespace URPMk2
{
	public class InterceptorCombatState : MLCombatState
	{
        private readonly InterceptorStateManager stateManager;
        public InterceptorCombatState(InterceptorStateManager mlManager)
        {
            this.mlManager = mlManager;
            this.stateManager = mlManager;
        }

        protected override void UpdateObservations()
        {
            stateManager.AgentObservations.EnemyMapPosition =
                target != null ?
                target.ObjTransform.position :
                new Vector3(-1f, -1f, -1f);
        }
    }
}
