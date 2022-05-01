using UnityEngine;

namespace URPMk2
{
    public class WeaponDamage : MonoBehaviour
    {
        private float funcCoeff, funcInter, penCoeff, penVar;
        private LayerMask layersToDamage;
        private Transform myTransform;
        private WeaponMaster weaponMaster;
        private void SetInit()
        {
            myTransform = transform;
            weaponMaster = GetComponent<WeaponMaster>();
        }
        private void Start()
        {
            myTransform = transform;
            GunDamageSettings damageSettings = weaponMaster.GetWeaponSettings().damageSettings;
            funcCoeff = damageSettings.funcCoeff;
            funcInter = damageSettings.funcInter;
            penCoeff = damageSettings.penCoeff;
            penVar = damageSettings.penVar;
            layersToDamage = damageSettings.layersToDamage;
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
        private float GetPenetration(float dmg)
        {
            float pen = dmg * penCoeff;
            return Random.Range(pen - penVar, pen + penVar);
        }
        private void HandleGunHit(RaycastHit hit)
        {
            if ((layersToDamage & (1 << hit.transform.gameObject.layer)) == 0)
                return;

            float dist = (myTransform.position - hit.transform.position).magnitude;
            if (dist < 1)
                dist = 1;
            // global damage handler should know about obj armour
            float dmg = dist * funcCoeff + funcInter;
            float pen = penCoeff == 0 ? 1 : GetPenetration(dmg);
            Debug.Log("Was hit: " + hit.transform.name + "dist: " + dist + " damage: " + dmg + " penetration: " + pen);
        }
    }
}
