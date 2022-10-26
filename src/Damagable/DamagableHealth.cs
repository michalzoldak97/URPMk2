using UnityEngine;

namespace URPMk2
{
    public class DamagableHealth : MonoBehaviour
    {
        public float health { get; private set; }
        private float baseHealth;
        private DamagableMaster dmgMaster;
        private void SetInit()
        {
            dmgMaster = GetComponent<DamagableMaster>();
        }
        private void Start()
        {
            health = dmgMaster.GetDamagableSettings().health;
            baseHealth = health;
        }
        private void OnEnable()
        {
            SetInit();
            dmgMaster.EventHitByGun += GetDamage;
            dmgMaster.EventHitByExplosion += GetDamage;
        }
        private void OnDisable()
        {
            dmgMaster.EventHitByGun -= GetDamage;
            dmgMaster.EventHitByExplosion -= GetDamage;
        }
        private void DoDestroyActions()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, GameConfig.secToDestroy);
        }
        private void GetDamage(DamageInfo dmgInfo)
        {
            
            float pDmg = dmgInfo.dmg > health ? health : dmgInfo.dmg;

            dmgMaster.CallEventReceivedDamage(pDmg);
            GlobalDamageMaster.RegisterDamage(dmgInfo.origin, pDmg);

            health -= dmgInfo.dmg;
            if (health < 0)
            {
                health = 0;
                dmgMaster.CallEventDestroyObject();
                DoDestroyActions();
                return;
            }
            if (dmgInfo.dmg < 0)
                dmgMaster.CallEventHealthRecovered();
            if (health < (baseHealth * .1f))
                dmgMaster.CallEventHealthLow();
        }
    }
}
