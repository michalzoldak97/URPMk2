using UnityEngine;

namespace URPMk2
{
	public class ItemPhysics : MonoBehaviour
	{
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			itemMaster.EventItemPickedUp += OnPickUp;
			itemMaster.EventItemThrow += OnThrow;
		}
		
		private void OnDisable()
		{
			itemMaster.EventItemPickedUp -= OnPickUp;
			itemMaster.EventItemThrow -= OnThrow;
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

			if (!itemMaster.GetItemSettings().deactivateCollOnPickUp)
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
