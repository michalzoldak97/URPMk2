using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URPMk2
{
	public class PlayerInventoryUIManager : MonoBehaviour
	{
		[SerializeField] Transform itemUIParent;
		[SerializeField] ItemButtonsSO itemButtons;
		private PlayerInventoryMaster inventoryMaster;
		private void SetInit()
		{
			inventoryMaster = GetComponent<PlayerInventoryMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			inventoryMaster.ItemPlaced += RebuildInventoryUI;
		}
		
		private void OnDisable()
		{
			inventoryMaster.ItemPlaced -= RebuildInventoryUI;
		}
		private void CallEventItemActivate(Transform item)
        {
			inventoryMaster.CallEventItemActivate(item);

		}
		private void SetUpWeaponButton(GameObject itemButton, ItemSettings itemSettings)
        {
			UIGunButton itemUIComponent = itemButton.GetComponent<UIGunButton>();
			itemUIComponent.SetItemName(itemSettings.toItemName);
			itemUIComponent.SetItemImage(itemSettings.itemImage);
		}
		private void AddItemButton(Transform item)
        {
			ItemSettings itemSettings = item.GetComponent<ItemMaster>().GetItemSettings();
			int itemTypeID = itemSettings.itemTypeID;
			GameObject itemButton = Instantiate(itemButtons.itemButtons[itemTypeID], itemUIParent);
			itemButton.GetComponent<Button>().onClick.AddListener(delegate { CallEventItemActivate(item); });
			if (itemTypeID == 0)
            {
				SetUpWeaponButton(itemButton, itemSettings);
            }
        }
		private void ClearUI()
		{
			foreach (Transform UIElement in itemUIParent)
			{
				Destroy(UIElement.gameObject);
			}
		}
		private void RebuildInventoryUI(Transform dummy)
        {
			ClearUI();
			List<Transform> items = inventoryMaster.GetItemList();
			for (int i = 0; i < items.Count; i++)
            {
				AddItemButton(items[i]);
			}
		}
	}
}
