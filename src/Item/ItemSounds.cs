using UnityEngine;

namespace URPMk2
{
	public class ItemSounds : MonoBehaviour
	{
		private int playerLayer;
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
        private void Start()
        {
            playerLayer = 1 << LayerMask.NameToLayer("Player");
		}
        private void OnEnable()
		{
			SetInit();
			itemMaster.EventItemPickedUp += PlayPickUpSound;
			itemMaster.EventItemThrow += PlayThrowSound;
		}
		
		private void OnDisable()
		{
			itemMaster.EventItemPickedUp -= PlayPickUpSound;
			itemMaster.EventItemThrow -= PlayThrowSound;
		}
		private void PlayPickUpSound(Transform dummy)
        {
			AudioSource.PlayClipAtPoint(itemMaster.GetItemSettings().pickUpSound, transform.position);
        }
		private void PlayThrowSound(Transform dummy)
        {
			AudioSource.PlayClipAtPoint(itemMaster.GetItemSettings().throwSound, transform.position);
		}
	}
}
