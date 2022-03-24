using UnityEngine;

namespace URPMk2
{
	public class ItemThrow : MonoBehaviour
	{
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			itemMaster.EventItemThrow += ThrowItem;
		}
		
		private void OnDisable()
		{
			itemMaster.EventItemThrow -= ThrowItem;
		}
		private void ThrowItem(Transform origin)
        {
			Debug.Log("The transform is: " + origin);
			if (GetComponent<Rigidbody>() != null)
				GetComponent<Rigidbody>().AddForce(origin.forward * 
					itemMaster.GetItemSettings().throwForce, ForceMode.Impulse);
		}
	}
}
