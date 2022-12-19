using UnityEngine;

namespace URPMk2
{
    public class DefenderCombatState : MLCombatState
    {
        private readonly DefenderStateManager stateManager;
        public DefenderCombatState(DefenderStateManager mlManager)
        {
            this.mlManager = mlManager;
            this.stateManager = mlManager;
        }

        protected override void UpdateObservations()
        {
            stateManager.AgentObservations.NumOfVisibleEnemies = numOfEnemies > 0 ?
				(numOfEnemies / mlManager.GetFSMSettings().enemiesBufferSize) : 0f;

            stateManager.AgentObservations.EnemyMapPosition =
                target != null ?
                target.ObjTransform.position :
                new Vector3(-1f, -1f, -1f);
        }
    }
}
