using UnityEngine;

namespace URPMk2
{
	public class ItemPositionRotation : MonoBehaviour
	{
		private ItemMaster _itemMaster; 
		private void SetInit()
		{
			_itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			_itemMaster.EventItemPickedUp += SetPosition;
			_itemMaster.EventItemPickedUp += SetRotation;
		}
		
		private void OnDisable()
		{
			_itemMaster.EventItemPickedUp -= SetPosition;
			_itemMaster.EventItemPickedUp -= SetRotation;
		}
		private void SetPosition(Transform origin)
        {
			float[] c = _itemMaster.GetItemSettings().onPlayerPosition;
			gameObject.transform.localPosition = new Vector3(c[0], c[1], c[2]);
        }
		private void SetRotation(Transform origin)
        {
			float[] c = _itemMaster.GetItemSettings().onPlayerRotation;
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(c[0], c[1], c[2]));
		}
	}
}
