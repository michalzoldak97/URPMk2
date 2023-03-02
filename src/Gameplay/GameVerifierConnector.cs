using UnityEngine;

namespace URPMk2
{
	public class GameVerifierConnector : MonoBehaviour
	{
		private DamagableMaster dmgMaster;
		private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
        }
		
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventDestroyObject += InformGameVerifier;
		}
		
		private void OnDisable()
		{
            dmgMaster.EventDestroyObject -= InformGameVerifier;
        }

		private void InformGameVerifier(Transform t)
		{
			GameVerifier.CargoDestroyed(transform);
		}
	}
}
