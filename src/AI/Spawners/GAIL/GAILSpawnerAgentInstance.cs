using UnityEngine;

namespace URPMk2
{
	public class GAILSpawnerAgentInstance : MonoBehaviour
	{
		private DamagableMaster dmgMaster;
		private GAILNPCGroupSpawner groupSpawner;
		private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
        }
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventDestroyObject += OnAgentDestroy;
		}
		
		private void OnDisable()
		{
			dmgMaster.EventDestroyObject -= OnAgentDestroy;
		}
		public void SetGroupSpawner(GAILNPCGroupSpawner groupSpawner)
		{
			this.groupSpawner = groupSpawner;
		}
		private void OnAgentDestroy(Transform killer)
		{
			groupSpawner.OnGroupAgentDestroyed();
		}
	}
}
