using UnityEngine;

namespace URPMk2
{
    public class DamagableHealth : MonoBehaviour
    {
        public float GetHealth() { return health; }

        private bool isHealthLow, isDestroyed;
        private float health;
        private DamagableMaster dmgMaster;
        private void SetInit()
        {
            dmgMaster = GetComponent<DamagableMaster>();
        }
        private void Start()
        {
            health = dmgMaster.GetDamagableSettings().health;
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
            if (!isHealthLow)
                dmgMaster.CallEventHealthLow();

            gameObject.SetActive(false);
            Destroy(gameObject, GameConfig.secToDestroy);
        }
        private void GetDamage(DamageInfo dmgInfo)
        {
            if (isDestroyed)
                return;

            float pDmg = dmgInfo.dmg > health ? health : dmgInfo.dmg;

            dmgMaster.CallEventReceivedDamage(dmgInfo.origin, pDmg);
            GlobalDamageMaster.RegisterDamage(dmgInfo.origin, pDmg);

            health -= dmgInfo.dmg;
            if (health < 0)
            {
                isDestroyed = true;
                health = 0; 
                GlobalDamageMaster.CallEventRegisterDestruction(dmgInfo.origin); // reward for destruction
                dmgMaster.CallEventDestroyObject(dmgInfo.origin);
                DoDestroyActions();
                return;
            }

            if (dmgInfo.dmg < 0)
            {
                dmgMaster.CallEventHealthRecovered();

                if (health > dmgMaster.GetDamagableSettings().lowHealth)
                    isHealthLow = false;
            }

            if (!isHealthLow &&
                health < dmgMaster.GetDamagableSettings().lowHealth)
            {
                dmgMaster.CallEventHealthLow();
                isHealthLow = true;
            }
        }
    }
}
