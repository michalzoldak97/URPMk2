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
		}
		
		private void OnDisable()
		{
			itemMaster.EventActivateOnParent -= OnActivate;
			itemMaster.EventDisableOnParent -= OnDeactivate;
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
		}
        private void OnTriggerStay(Collider other)
        {
			if (inventoryMaster != null && shouldInformCamera)
			{
				inventoryMaster.itemCamera.gameObject.SetActive(true);
				shouldInformCamera = false;
			}
        }
		private void OnTriggerExit(Collider other)
		{
			if (inventoryMaster != null)
			{
				inventoryMaster.itemCamera.gameObject.SetActive(false);
				shouldInformCamera = true;
			}
		}
	}
}
