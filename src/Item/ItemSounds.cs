using UnityEngine;

namespace URPMk2
{
	public class ItemSounds : MonoBehaviour
	{
		private int playerLayer;
		private AudioClip[] hitSounds;
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
		}
        private void Start()
        {
            playerLayer = LayerMask.NameToLayer("Player");
			hitSounds = itemMaster.GetItemSettings().collisionSounds;
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
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != playerLayer)
            {
				AudioSource.PlayClipAtPoint(hitSounds[Random.Range(0, hitSounds.Length - 1)], transform.position);
			}
        }
    }
}
