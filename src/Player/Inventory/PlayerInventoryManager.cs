using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class PlayerInventoryManager : MonoBehaviour
	{
		private Transform selectedItem;
		private List<Transform> inventoryItems = new List<Transform>();
		public List<Transform> GetInventoryItems() { return inventoryItems; }
		private PlayerInventoryMaster inventoryMaster;
		private void SetInit()
		{
			inventoryMaster = GetComponent<PlayerInventoryMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			inventoryMaster.EventItemPickUp += AddItem;
			inventoryMaster.EventItemActivate += ActivateItem;
		}
		
		private void OnDisable()
		{
			inventoryMaster.EventItemPickUp -= AddItem;
			inventoryMaster.EventItemActivate -= ActivateItem;
		}
		private void AddItem(Transform item)
        {
			if (inventoryItems.Contains(item))
				return;

			inventoryItems.Add(item);
			inventoryMaster.CallEventItemPlaced(item);

			if (selectedItem == null)
				inventoryMaster.CallEventItemActivate(item);
        }
		private void ActivateItem(Transform item)
        {
			if (!inventoryItems.Contains(item))
				return;

			if (selectedItem != null)
			{
				ItemMaster currentItemMaster = selectedItem.GetComponent<ItemMaster>();
				currentItemMaster.CallEventDisableOnParent();
				if (currentItemMaster.GetItemSettings().deactivateObjOnPickUp)
					selectedItem.gameObject.SetActive(false);
			}

			item.gameObject.SetActive(true);
			item.GetComponent<ItemMaster>().CallEventActivateOnParent();
			selectedItem = item;
		}
	}
}
