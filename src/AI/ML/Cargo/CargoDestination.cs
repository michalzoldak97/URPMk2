using UnityEngine;

namespace URPMk2
{
	public class CargoDestination : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<CargoController>() == null)
				return;

			other.gameObject.GetComponent<DamagableMaster>().CallEventDestroyObject(transform);
			Destroy(other.gameObject, GameConfig.secToDestroy);
			other.gameObject.SetActive(false);
        }
	}
}
