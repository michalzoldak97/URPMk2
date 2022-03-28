using UnityEngine;

namespace URPMk2
{
	public class ItemToggleScripts : MonoBehaviour
	{
		[SerializeField] private MonoBehaviour[] scripts;
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
        private void Start()
        {
			DeactivateMonoScripts(transform);
		}

        private void OnEnable()
		{
			SetInit();
			itemMaster.EventItemPickedUp += ActivateMonoScripts;
			itemMaster.EventItemThrow += DeactivateMonoScripts;
		}
		
		private void OnDisable()
		{
			itemMaster.EventItemPickedUp -= ActivateMonoScripts;
			itemMaster.EventItemThrow -= DeactivateMonoScripts;
		}
		private void ActivateMonoScripts(Transform dummy)
		{
			Debug.Log("Activate for: " + transform.name);
			foreach (MonoBehaviour script in scripts)
			{
				script.enabled = true;
			}
		}
		private void DeactivateMonoScripts(Transform dummy)
        {
			Debug.Log("Deactivate for: " + transform.name);
			foreach (MonoBehaviour script in scripts)
            {
				script.enabled = false;
            }
        }
	}
}
