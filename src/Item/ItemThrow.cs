using UnityEngine;
using System.Collections;

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
			itemMaster.EventItemThrow += StartThrowItem;
		}
		
		private void OnDisable()
		{
			itemMaster.EventItemThrow -= StartThrowItem;
		}
		private void StartThrowItem(Transform origin)
        {
			StartCoroutine(ThrowItem(origin));
        }
		private IEnumerator ThrowItem(Transform origin)
        {
			yield return new WaitForEndOfFrame();
			if (GetComponent<Rigidbody>() != null)
				GetComponent<Rigidbody>().AddForce(origin.forward * 
					itemMaster.GetItemSettings().throwForce, ForceMode.Impulse);
		}
	}
}
