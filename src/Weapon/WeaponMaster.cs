using UnityEngine;

namespace URPMk2
{
    public class WeaponMaster : MonoBehaviour
    {
        [SerializeField] private WeaponSettingsSO weaponSettings;
        public WeaponSettingsSO GetWeaponSettings() { return weaponSettings; }

        public bool isAim;
        public bool isReloading { get; private set; }
        public void SetIsReloading(bool isReloading)
        {
            this.isReloading = isReloading;
        }
        public bool isShootState { get; private set; }
        public void SetIsShootState(bool isShootState)
        {
            this.isShootState = isShootState;
        }
        public bool isShootingBurst { get; private set; }
        public void SetShootingBurst(bool isShootingBurst)
        {
            this.isShootingBurst = isShootingBurst;
        }
        public bool isTriggerLocked { get; private set; }
        public void SetIsTriggerLocked(bool isTriggerLocked)
        {
            this.isTriggerLocked = isTriggerLocked;
        }
        public bool isWeaponLoaded { get; private set; }
        public void SetIsWeaponLoaded(bool isWeaponLoaded)
        {
            //Debug.Log("Setting is weapon loaded to: " + isWeaponLoaded);
            this.isWeaponLoaded = isWeaponLoaded;
        }
        public WeaponFireMode fireMode;
        public Transform dmgOrigin { get; private set; }
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

        public delegate void WeaponHandGrenadeEventHandler(float force, float xAngle);
        public event WeaponHandGrenadeEventHandler EventThrowGrenade;

        public void SetDmgOrigin(Transform origin)
        {
            if (origin == dmgOrigin)
                return;
            dmgOrigin = origin;
        }
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
        public void CallEventThrowGrenade(float force, float angle)
        {
            EventThrowGrenade?.Invoke(force, angle);
        }
        private void Start()
        {
            fireMode = (WeaponFireMode)weaponSettings.gunSettings.defaultFireMode;
            isWeaponLoaded = true;
        }
    }
}
