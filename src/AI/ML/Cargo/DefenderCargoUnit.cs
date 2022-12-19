using UnityEngine;

namespace URPMk2
{
	public class DefenderCargoUnit : MonoBehaviour, ICargoUnit
	{
		private DefenderAgent defenderAgent;

		private void Awake()
		{
			defenderAgent = GetComponent<DefenderAgent>();
		}

		public void SetCargoParent(Transform cargoParent)
		{
			defenderAgent.SetCargoParent(cargoParent);
			// TODO: connect with cargo dmg master
			// on dmg add negative reward
			// on destroy serach new parent
			// or go to the final dest
		}
	}
}
