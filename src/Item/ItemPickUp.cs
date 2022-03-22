using UnityEngine;

namespace URPMk2
{
	public class ItemPickUp : MonoBehaviour
	{
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			itemMaster.EventInteractionRequested += HandlePickUp;
		}
		
		private void OnDisable()
		{
			itemMaster.EventInteractionRequested -= HandlePickUp;
		}
		private void HandlePickUp(Transform origin)
        {
			if (origin.root.GetComponent<IInventoryMaster>() == null)
				return;

			gameObject.transform.SetParent(origin);
			itemMaster.CallEventItemPickedUp(origin);
			origin.root.GetComponent<IInventoryMaster>().CallEventItemPickUp(gameObject.transform);
			// set obj state
			if (!itemMaster.GetItemSettings().deactivateObjOnPickUp)
				return;
			gameObject.SetActive(false);
		}
	}
}
