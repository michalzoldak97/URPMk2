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
			gameObject.transform.localPosition = Utils.GetVector3FromFloat(c);
        }
		private void SetRotation(Transform origin)
        {
			float[] c = itemMaster.GetItemSettings().onPlayerRotation;
			gameObject.transform.localRotation = Quaternion.Euler(Utils.GetVector3FromFloat(c));
		}
	}
}
