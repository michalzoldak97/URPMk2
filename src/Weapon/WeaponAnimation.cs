using UnityEngine;

namespace URPMk2
{
	public class WeaponAnimation : MonoBehaviour
	{
		[SerializeField] private Transform animatedMesh;
		private Animator animator;
		private WeaponMaster weaponMaster;
		private ItemMaster itemMaster;
 		private void SetInit()
		{
			animator = animatedMesh.GetComponent<Animator>();
			weaponMaster = GetComponent<WeaponMaster>();
			itemMaster = GetComponent<ItemMaster>();
		}
        private void Start()
        {
			animator.enabled = false;
		}
        private void OnEnable()
		{
			SetInit();
			itemMaster.EventItemPickedUp += EnableAnimator;
			itemMaster.EventItemThrow += DisableAnimator;
			weaponMaster.EventShoot += PlayShootAnimation;
			weaponMaster.EventStartReload += PlayReloadAnimation;
		}
		
		private void OnDisable()
		{
			itemMaster.EventItemPickedUp -= EnableAnimator;
			itemMaster.EventItemThrow -= DisableAnimator;
			weaponMaster.EventShoot -= PlayShootAnimation;
			weaponMaster.EventStartReload -= PlayReloadAnimation;
		}
		private void PlayShootAnimation()
        {
			animator.SetTrigger(GameConfig.shootKey);
        }
		private void PlayReloadAnimation()
		{
			animator.SetTrigger(GameConfig.reloadKey);
		}
		private void EnableAnimator(Transform dummy)
        {
			animator.enabled = true;
		}
		private void DisableAnimator(Transform dumm)
		{
			animatedMesh.localPosition = Vector3.zero;
			animatedMesh.localRotation = Quaternion.Euler(0f, 0f, 0f);
			animator.enabled = false;
		}
	}
}
