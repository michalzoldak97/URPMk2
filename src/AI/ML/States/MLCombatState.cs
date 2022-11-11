using UnityEngine;

namespace URPMk2
{
	public class MLCombatState : IMLState
	{
		private readonly MLStateManager mlManager;
		public MLCombatState(MLStateManager mlManager)
		{
			this.mlManager = mlManager;
		}
        public void UpdateState()
		{
			Debug.Log("Im in combat state");
		}
	}
}
