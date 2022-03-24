using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
			InputManager.playerInputActions.Humanoid.ItemThrow.performed += CallEventItemThrow;
			InputManager.playerInputActions.Humanoid.ItemThrow.Enable();
			inventoryMaster.EventItemPickUp += AddItem;
			inventoryMaster.EventItemActivate += ActivateItem;
			inventoryMaster.EventItemThrow += ThrowItem;
		}
		
		private void OnDisable()
		{
			InputManager.playerInputActions.Humanoid.ItemThrow.performed -= CallEventItemThrow;
			InputManager.playerInputActions.Humanoid.ItemThrow.Disable();
			inventoryMaster.EventItemPickUp -= AddItem;
			inventoryMaster.EventItemActivate -= ActivateItem;
			inventoryMaster.EventItemThrow -= ThrowItem;
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

			// deactivate current item
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
		private void CallEventItemThrow(InputAction.CallbackContext obj)
        {
			if (selectedItem != null)
				inventoryMaster.CallEventItemThrow(selectedItem);
        }
		private void ThrowItem(Transform item)
        {
			if (!inventoryItems.Contains(item))
				return;

			ItemMaster currentItemMaster = item.GetComponent<ItemMaster>();
			Transform origin = item.parent;

			item.SetParent(null);
			currentItemMaster.CallEventDisableOnParent();
			currentItemMaster.CallEventItemThrow(origin);
			inventoryMaster.CallEventItemThrow(item);
		}
	}
}
