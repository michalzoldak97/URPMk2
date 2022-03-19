using UnityEngine;

namespace URPMk2
{
	public class ItemPhysics : MonoBehaviour
	{
		private ItemMaster _itemMaster;
		private void SetInit()
		{
			_itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			_itemMaster.EventItemPickedUp += OnPickUp;
			_itemMaster.EventItemThrow += OnThrow;
		}
		
		private void OnDisable()
		{
			_itemMaster.EventItemPickedUp -= OnPickUp;
			_itemMaster.EventItemThrow -= OnThrow;
		}
		private void ToggleItemPhysics(bool toState)
        {
			foreach (Rigidbody rb in GetComponents<Rigidbody>())
			{
				rb.isKinematic = !toState;
				rb.useGravity = toState;
			}
			foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
			{
				rb.isKinematic = !toState;
				rb.useGravity = toState;
			}

			if (!_itemMaster.GetItemSettings().deactivateCollOnPickUp)
				return;

			foreach (Collider col in GetComponents<Collider>())
			{
				col.isTrigger = !toState;
			}
			foreach (Collider col in GetComponentsInChildren<Collider>())
			{
				col.isTrigger = !toState;
			}
		}
		private void OnPickUp(Transform origin)
        {
			ToggleItemPhysics(false);
		}
		private void OnThrow(Transform origin)
		{
			ToggleItemPhysics(true);
		}
	}
}
