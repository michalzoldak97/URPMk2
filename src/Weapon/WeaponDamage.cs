using UnityEngine;

namespace URPMk2
{
    public class WeaponDamage : MonoBehaviour
    {
        private WeaponMaster weaponMaster;
        private void SetInit()
        {
            weaponMaster = GetComponent<WeaponMaster>();
        }
        private void OnEnable()
        {
            SetInit();
            weaponMaster.EventHitByGun += HandleGunHit;
        }
        private void OnDisable()
        {
            weaponMaster.EventHitByGun -= HandleGunHit;
        }
        private void HandleGunHit(RaycastHit hit)
        {
            Debug.Log(hit.transform.name);
        }
    }
}
