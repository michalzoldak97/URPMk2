using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class PlayerInventoryUIManager : MonoBehaviour
	{
		[SerializeField] ItemButtonsSO itemButtons;
		[SerializeField] Transform itemUIParent;
		[SerializeField] GameObject inventoryUIParent;
		private bool isInventoryUIActive;
		private PlayerInventoryMaster inventoryMaster;
		private void SetInit()
		{
			inventoryMaster = GetComponent<PlayerInventoryMaster>();
		}

        private void Start()
        {
			ToggleInventoryUI(false);
		}

        private void OnEnable()
		{
			SetInit();
			inventoryMaster.EventItemPlaced += RebuildInventoryUI;
			inventoryMaster.EventItemThrow += RebuildInventoryUI;
			inventoryMaster.EventItemActivate += StartMarkButtonActive;
			InputManager.playerInputActions.Humanoid.ToggleInventory.performed += EnableInventoryUI;
			InputManager.playerInputActions.Humanoid.ToggleInventory.Enable();
			InputManager.playerInputActions.UI.ToggleInventory.performed += DisableInventoryUI;
			InputManager.playerInputActions.UI.ToggleInventory.Enable();
		}
		
		private void OnDisable()
		{
			inventoryMaster.EventItemPlaced -= RebuildInventoryUI;
			inventoryMaster.EventItemThrow -= RebuildInventoryUI;
			inventoryMaster.EventItemActivate -= StartMarkButtonActive;
			InputManager.playerInputActions.Humanoid.ToggleInventory.performed -= EnableInventoryUI;
			InputManager.playerInputActions.Humanoid.ToggleInventory.Disable();
			InputManager.playerInputActions.UI.ToggleInventory.performed -= DisableInventoryUI;
			InputManager.playerInputActions.UI.ToggleInventory.Disable();
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
			int itemCount = items.Count;
			for (int i = 0; i < itemCount; i++)
            {
				AddItemButton(items[i]);
			}
			// mark selected item button
			for (int i = 0; i < itemCount; i++)
			{
				if (items[i].GetComponent<ItemMaster>().isSelectedOnParent)
					StartMarkButtonActive(items[i]);
			}
		}
		private void StartMarkButtonActive(Transform item)
        {
			StartCoroutine(MarkButtonActive(item));
        }
		private IEnumerator MarkButtonActive(Transform item)
        {
			yield return new WaitForEndOfFrame();
			List<Transform> items = inventoryMaster.GetItemList();
			int activeItemIdx = items.FindIndex(i => i == item);
			if (activeItemIdx == -1)
				yield break;

			int buttonCount = 0;
            foreach (Transform UIElement in itemUIParent)
            {
                if (buttonCount != activeItemIdx)
                    UIElement.GetComponent<Image>().color = UIElement.GetComponent<Button>().colors.normalColor;
                else
                    UIElement.GetComponent<Image>().color = UIElement.GetComponent<Button>().colors.pressedColor;
                buttonCount++;
            }
        }
		private void ToggleInventoryUI(bool toState)
        {
			CursorManager.ToggleCursorState(toState);
			if (toState)
				InputManager.ToggleActionMap(InputManager.playerInputActions.UI);
			else
				InputManager.ToggleActionMap(InputManager.playerInputActions.Humanoid);
			if (isInventoryUIActive)
			{
				InputManager.playerInputActions.UI.Enable();
				InputManager.playerInputActions.Humanoid.Disable();
			}
			else
			{
				InputManager.playerInputActions.UI.Disable();
				InputManager.playerInputActions.Humanoid.Enable();
			}
			inventoryUIParent.SetActive(toState);
		}
		private void EnableInventoryUI(InputAction.CallbackContext obj)
        {
			isInventoryUIActive = !isInventoryUIActive;
			ToggleInventoryUI(isInventoryUIActive);
		}
		private void DisableInventoryUI(InputAction.CallbackContext obj)
		{
			isInventoryUIActive = !isInventoryUIActive;
			ToggleInventoryUI(isInventoryUIActive);
		}
	}
}
