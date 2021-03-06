using UnityEngine;

namespace URPMk2
{
    public class WeaponMaster : MonoBehaviour
    {
        [SerializeField] private WeaponSettingsSO weaponSettings;
        public WeaponSettingsSO GetWeaponSettings() { return weaponSettings; }

        public bool isAim;
        public bool isReloading;
        public bool isShootState, isShootingBurst;
        public bool isWeaponLoaded = true;
        public WeaponFireMode fireMode;
        public delegate void WeaponInputEvenHandler();
        public event WeaponInputEvenHandler EventAimRequest;
        public event WeaponInputEvenHandler EventPullTrigger;
        public event WeaponInputEvenHandler EventReleaseTrigger;
        public event WeaponInputEvenHandler EventReloadRequest;
        public event WeaponInputEvenHandler EventFireModeChangeRequest;
        public event WeaponInputEvenHandler EventFireModeChanged;

        public delegate void WeaponEventHandler();
        public event WeaponEventHandler EventShoot;
        public event WeaponEventHandler EventStartReload;
        public event WeaponEventHandler EventReload;
        public event WeaponEventHandler EventUpdateAmmoUI;

        public delegate void WeaponDamageEventHandler(RaycastHit hit);
        public event WeaponDamageEventHandler EventHitByGun;

        public void CallEventAimRequest()
        {
            EventAimRequest?.Invoke();
        }
        public void CallEventPullTrigger()
        {
            EventPullTrigger?.Invoke();
        }
        public void CallEventReleaseTrigger()
        {
            EventReleaseTrigger?.Invoke();
        }
        public void CallEventReloadRequest()
        {
            EventReloadRequest?.Invoke();
        }
        public void CallEventShoot()
        {
            EventShoot?.Invoke();
        }
        public void CallEventStartReload()
        {
            EventStartReload?.Invoke();
        }
        public void CallEventReload()
        {
            EventReload?.Invoke();
        }
        public void CallEventFireModeChangeRequest()
        {
            EventFireModeChangeRequest?.Invoke();
        }
        public void CallEventFireModeChanged()
        {
            EventFireModeChanged?.Invoke();
        }
        public void CallEventEventUpdateAmmoUI()
        {
            EventUpdateAmmoUI?.Invoke();
        }
        public void CallEventHitByGun(RaycastHit hit)
        {
            EventHitByGun?.Invoke(hit);
        }
        private void Start()
        {
            fireMode = (WeaponFireMode)weaponSettings.gunSettings.defaultFireMode;
        }
    }
}
