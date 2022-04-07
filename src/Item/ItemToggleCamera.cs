using UnityEngine;

namespace URPMk2
{
	public class ItemToggleCamera : MonoBehaviour
	{
		private bool shouldInformCamera;
		private ItemMaster itemMaster;
		private PlayerInventoryMaster inventoryMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			itemMaster.EventActivateOnParent += OnActivate;
			itemMaster.EventDisableOnParent += OnDeactivate;
			itemMaster.EventToggleItemCamera += ChangeItemCameraState;
		}
		
		private void OnDisable()
		{
			itemMaster.EventActivateOnParent -= OnActivate;
			itemMaster.EventDisableOnParent -= OnDeactivate;
			itemMaster.EventToggleItemCamera -= ChangeItemCameraState;
		}
		private void OnActivate()
        {
			if (inventoryMaster == null)
				inventoryMaster = transform.root.GetComponent<PlayerInventoryMaster>();
			shouldInformCamera = true;
		}
		private void OnDeactivate()
		{
			shouldInformCamera = false;
			inventoryMaster = null;
		}
		private void ChangeItemCameraState(bool toState)
        {
			inventoryMaster.itemCamera.gameObject.SetActive(toState);
			shouldInformCamera = !toState;
		}
        private void OnTriggerStay(Collider other)
        {
			if (inventoryMaster != null && shouldInformCamera)
				ChangeItemCameraState(true);
		}
		private void OnTriggerExit(Collider other)
		{
			if (inventoryMaster != null)
				ChangeItemCameraState(false);
		}
	}
}
