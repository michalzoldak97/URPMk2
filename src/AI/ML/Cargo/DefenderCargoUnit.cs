using UnityEngine;

namespace URPMk2
{
	public class DefenderCargoUnit : MonoBehaviour, ICargoUnit
	{
		private Transform cargoParentDMGMaster;
		private DefenderStateManager dsManager;

		private void Start()
		{
			agentObservations = GetComponent<DefenderStateManager>();
		}

		public void SetCargoParent(Transform cargoParent)
		{
			dsManager.SetCargoParent(cargoParent);
			cargoParentDMGMaster = cargoParent.GetComponent<DamagableMaster>();
			cargoParentDMGMaster.EveentDamageObject += OnCargoParentDamage;
			cargoParentDMGMaster.EventDestroyObject += OnCargoParentDestroy;
			//TODO: on destroy set parent
			// states should determine observed parent position
		}
	}
}
