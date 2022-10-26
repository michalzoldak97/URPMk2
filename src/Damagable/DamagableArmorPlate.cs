using UnityEngine;

namespace URPMk2
{
    public class DamagableArmorPlate : MonoBehaviour
    {
        [SerializeField] private DamagableMaster parentDmgMaster;
        private DamageInfo dmgInfoToPass;
        private DamagableMaster dmgMaster;

        private void SetInit()
        {
            dmgInfoToPass = new DamageInfo(DamageType.Explosion, transform);
            dmgMaster = GetComponent<DamagableMaster>();
            if (parentDmgMaster == null &&
                transform.root.GetComponent<DamagableMaster>() != null)
                parentDmgMaster = transform.root.GetComponent<DamagableMaster>();

            if (parentDmgMaster == dmgMaster ||
                parentDmgMaster == null)
            {
                Debug.Log("No parent dmg master found");
                Destroy(this);
            }
        }
        private void OnEnable()
        {
            SetInit();
            dmgMaster.EventHitByGun += TransferGunDamage;
            dmgMaster.EventHitByExplosion += TransferExplosionDamage;
        }
        private void OnDisable()
        {
            dmgMaster.EventHitByGun -= TransferGunDamage;
            dmgMaster.EventHitByExplosion -= TransferExplosionDamage;
        }
        private void TransferGunDamage(DamageInfo dmgInfo)
        {
            float myArmor = dmgMaster.GetArmor();
            float pen = dmgInfo.pen - myArmor;
            float dmg = (1 - (myArmor / pen)) * dmgInfo.dmg;

            dmgInfoToPass.toDmg = dmgInfo.toDmg;
            dmgInfoToPass.origin = dmgInfo.origin;
            dmgInfoToPass.hit = dmgInfo.hit;
            dmgInfoToPass.damageType = DamageType.Gun;
            dmgInfoToPass.pen = pen;
            dmgInfoToPass.dmg = dmg;

            parentDmgMaster.CallEventHitByGun(dmgInfoToPass);

        }
        private void TransferExplosionDamage(DamageInfo dmgInfo)
        {
            float myArmor = dmgMaster.GetArmor();

            if (dmgInfo.pen < myArmor)
                return;
            
            float pen = dmgInfo.pen - myArmor;
            float dmg = (1 - (myArmor / pen)) * dmgInfo.dmg;

            dmgInfoToPass.toDmg = dmgInfo.toDmg;
            dmgInfoToPass.origin = dmgInfo.origin;
            dmgInfoToPass.damageType = DamageType.Explosion;
            dmgInfoToPass.pen = pen;
            dmgInfoToPass.dmg = dmg;

            parentDmgMaster.CallEventHitByExplosion(dmgInfoToPass);
        }
    }
}