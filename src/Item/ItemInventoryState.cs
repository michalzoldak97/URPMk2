using UnityEngine;

namespace URPMk2
{
	public class ItemInventoryState : MonoBehaviour
	{
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			itemMaster.EventActivateOnParent += SetItemSelected;
			itemMaster.EventDisableOnParent += SetItemUnselected;
		}
		
		private void OnDisable()
		{
			itemMaster.EventActivateOnParent -= SetItemSelected;
			itemMaster.EventDisableOnParent -= SetItemUnselected;
		}
		private void SetItemSelected()
		{
			itemMaster.isSelectedOnParent = true;
		}
		private void SetItemUnselected()
        {
			itemMaster.isSelectedOnParent = false;
		}
	}
}
