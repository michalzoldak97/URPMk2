using UnityEngine;

namespace URPMk2
{
	public class ItemPickUp : MonoBehaviour
	{
		private ItemMaster _itemMaster;
		private void SetInit()
		{
			_itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			_itemMaster.EventInteractionRequested += HandlePickUp;
		}
		
		private void OnDisable()
		{
			_itemMaster.EventInteractionRequested -= HandlePickUp;
		}
		private void HandlePickUp(Transform origin)
        {
			if (origin.root.GetComponent<IInventoryMaster>() == null)
				return;

			gameObject.transform.SetParent(origin);
			_itemMaster.CallEventItemPickedUp(origin);
        }
	}
}
