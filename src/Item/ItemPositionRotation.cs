using UnityEngine;

namespace URPMk2
{
	public class ItemPositionRotation : MonoBehaviour
	{
		private ItemMaster itemMaster; 
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			itemMaster.EventItemPickedUp += SetPosition;
			itemMaster.EventItemPickedUp += SetRotation;
		}
		
		private void OnDisable()
		{
			itemMaster.EventItemPickedUp -= SetPosition;
			itemMaster.EventItemPickedUp -= SetRotation;
		}
		private void SetPosition(Transform origin)
        {
			float[] c = itemMaster.GetItemSettings().onPlayerPosition;
			gameObject.transform.localPosition = new Vector3(c[0], c[1], c[2]);
        }
		private void SetRotation(Transform origin)
        {
			float[] c = itemMaster.GetItemSettings().onPlayerRotation;
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(c[0], c[1], c[2]));
		}
	}
}
