using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class PlayerItemDetector : MonoBehaviour
	{
		[SerializeField] private LayerMask _itemLayer;
		[SerializeField] private LayerMask _playerLayer;
		[SerializeField] private Transform _fpsCamera;
		private bool _isItemInRange;
		private int _ignorePlayerlayerMask;
		private float _nextCheck, _checkRate;
		private float[] _itemLabelWidthHeight;
		private Transform _itemInRange;
		private Rect _labelRect;
		private GUIStyle _labelStyle = new GUIStyle();
		private PlayerInventoryMaster _inventoryMaster;
		private void SetInit()
		{
			_inventoryMaster = GetComponent<PlayerInventoryMaster>();
			PlayerInventorySettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerInventorySettings;
			_checkRate = playerSettings.itemCheckRate;
			_itemLabelWidthHeight = playerSettings.itemLabelWidthHeight;
			_labelRect = new Rect(Screen.width / 2 - _itemLabelWidthHeight[0], 
				Screen.height / 2, _itemLabelWidthHeight[0], _itemLabelWidthHeight[1]);
			_labelStyle.fontSize = playerSettings.labelFontSize;
			_labelStyle.normal.textColor = Color.white;
			_ignorePlayerlayerMask = 1 << Utils.GetLayerIndexFromSingleLayer(_playerLayer);
			_ignorePlayerlayerMask = ~_ignorePlayerlayerMask;
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
			if (_isItemInRange && _itemInRange.GetComponent<ItemMaster>() != null)
            {
				_itemInRange.GetComponent<ItemMaster>().CallEventInteractionReqiested(_fpsCamera);
			}
        }
        private void ManageItemSearch()
        {
			if(!_isItemInRange && Time.time > _nextCheck)
            {
				DetectItem();
				_nextCheck = Time.time + _checkRate;
			}
			else if (_isItemInRange)
            {
				DetectItem();
			}
        }

		private bool CheckItemVisible(Transform itemTransform)
        {
			if (Physics.Linecast(_fpsCamera.position, itemTransform.position, out RaycastHit hit, _ignorePlayerlayerMask))
			{
				if (hit.transform != itemTransform)
                {
					return false;
				}
				else
					return true;
			}

			return true;
        }

		private void DetectItem()
        {
			if (Physics.SphereCast(_fpsCamera.position, 0.5f, _fpsCamera.forward, out RaycastHit hit, 3, _itemLayer))
			{
				Transform foundItem = hit.transform;
				if (foundItem != _itemInRange && CheckItemVisible(foundItem))
                {
					_isItemInRange = true;
					_itemInRange = foundItem;
				}
			}
			else
			{
				_isItemInRange = false;
				_itemInRange = null;
			}
		}

        private void Update()
        {
			ManageItemSearch();
		}
        private void OnGUI()
        {
            if (_isItemInRange)
            {
				GUI.Label(_labelRect, _itemInRange.name, _labelStyle);
            }
        }
    }
}
