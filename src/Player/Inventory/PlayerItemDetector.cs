using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class PlayerItemDetector : MonoBehaviour
	{
		[SerializeField] private Transform fpsCamera;
		private bool isItemInRange;
		private int ignorePlayerlayerMask;
		private float nextCheck, checkRate;
		private float[] itemLabelWidthHeight;
		private LayerMask itemLayer;
		private Transform itemInRange;
		private Rect labelRect;
		private GUIStyle labelStyle = new GUIStyle();
		private void SetInit()
		{
			PlayerInventorySettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerInventorySettings;
			checkRate = playerSettings.itemCheckRate;
			itemLabelWidthHeight = playerSettings.itemLabelWidthHeight;
			labelRect = new Rect(Screen.width / 2 - itemLabelWidthHeight[0], 
				Screen.height / 2, itemLabelWidthHeight[0], itemLabelWidthHeight[1]);
			labelStyle.fontSize = playerSettings.labelFontSize;
			labelStyle.normal.textColor = Color.white;

			itemLayer = 1 << LayerMask.NameToLayer("Item");
			// mask to ignore player layer when checking if the item is visible
			ignorePlayerlayerMask = 1 << LayerMask.NameToLayer("Player");
			ignorePlayerlayerMask = ~ignorePlayerlayerMask;
		}
		
		private void OnEnable()
		{
			SetInit();
			InputManager.playerInputActions.Humanoid.ItemInteract.performed += InteractWithItem;
			InputManager.playerInputActions.Humanoid.ItemInteract.Enable();
		}
        private void OnDisable()
        {
			InputManager.playerInputActions.Humanoid.ItemInteract.performed -= InteractWithItem;
			InputManager.playerInputActions.Humanoid.ItemInteract.Disable();
		}
		private void InteractWithItem(InputAction.CallbackContext obj)
        {
			if (isItemInRange && itemInRange.GetComponent<ItemMaster>() != null)
            {
				itemInRange.GetComponent<ItemMaster>().CallEventInteractionRequested(fpsCamera);
			}
        }
        private void ManageItemSearch()
        {
			if(!isItemInRange && Time.time > nextCheck)
            {
				DetectItem();
				nextCheck = Time.time + checkRate;
			}
			else if (isItemInRange)
            {
				DetectItem();
			}
        }

		private bool CheckItemVisible(Transform itemTransform)
        {
			if (Physics.Linecast(fpsCamera.position, itemTransform.position, out RaycastHit hit, ignorePlayerlayerMask))
			{
				if (hit.transform != itemTransform)
					return false;
				else
					return true;
			}

			return true;
        }

		private void DetectItem()
        {
			if (Physics.SphereCast(fpsCamera.position, 0.5f, fpsCamera.forward, out RaycastHit hit, 3, itemLayer))
			{
				Transform foundItem = hit.transform;
				if (foundItem != itemInRange && CheckItemVisible(foundItem))
                {
					isItemInRange = true;
					itemInRange = foundItem;
				}
			}
			else
			{
				isItemInRange = false;
				itemInRange = null;
			}
		}

        private void Update()
        {
			ManageItemSearch();
		}
        private void OnGUI()
        {
            if (isItemInRange)
            {
				GUI.Label(labelRect, itemInRange.name, labelStyle);
            }
        }
    }
}
