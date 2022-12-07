using UnityEngine;

namespace URPMk2
{
	public class SingleAgentTarget
	{
		public SingleAgentTarget(GameObject agent, SingleAgentTargetSpawner spawner)
		{
			Agent = agent;
			dmgMaster = agent.GetComponent<DamagableMaster>();
			mySpawner = spawner;

			dmgMaster.EventDestroyObject += InformSpawner;
		}
		public GameObject Agent { get; private set; }
		private readonly DamagableMaster dmgMaster;
		private readonly SingleAgentTargetSpawner mySpawner;

		private void InformSpawner(Transform killer)
		{
			mySpawner.TargetDestroyed();
		}
    }
}
