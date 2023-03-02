using UnityEngine;

namespace URPMk2
{
	public class ItemAmmo : MonoBehaviour
	{
		[SerializeField] private int amount;
		[SerializeField] private string ammoCode;
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
        }
		
		private void OnEnable()
		{
			SetInit();
			itemMaster.EventInteractionRequested += AddAmmo;
		}
		
		private void OnDisable()
		{
            itemMaster.EventInteractionRequested -= AddAmmo;
        }

		private void AddAmmo(Transform origin)
		{
			if (origin.root.GetComponent<PlayerMaster>() == null)
				return;

			origin.root.GetComponent<PlayerMaster>().CallEventAmmoChange(ammoCode, amount, null);

			Destroy(gameObject, GameConfig.secToDestroy);

			gameObject.SetActive(false);
        }
	}
}
