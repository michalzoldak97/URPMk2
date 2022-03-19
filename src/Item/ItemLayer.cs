using UnityEngine;

namespace URPMk2
{
	public class ItemLayer : MonoBehaviour
	{
		private int _originalLayer;
		private ItemMaster _itemMaster;
		private void SetInit()
		{
			_itemMaster = GetComponent<ItemMaster>();
			_originalLayer = gameObject.layer;
		}
		
		private void OnEnable()
		{
			SetInit();
			_itemMaster.EventItemPickedUp += ChangeOnPickUp;
			_itemMaster.EventItemThrow += ChangeOnThrow;
		}
		
		private void OnDisable()
		{
			_itemMaster.EventItemPickedUp -= ChangeOnPickUp;
			_itemMaster.EventItemThrow -= ChangeOnThrow;
		}
		private void SetLayer(int toSet)
        {
			gameObject.layer = toSet;
			foreach (Transform child in transform)
			{
				child.gameObject.layer = toSet;
			}
		}
		private void ChangeOnPickUp(Transform origin)
        {
			int layerToSet = LayerMask.NameToLayer(_itemMaster.GetItemSettings().toLayerName);
			SetLayer(layerToSet);
		}
		private void ChangeOnThrow(Transform origin)
        {
			SetLayer(_originalLayer);
		}
	}
}
