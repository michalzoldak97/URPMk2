using UnityEngine;

namespace URPMk2
{
	public class DamagablePlayerHealth : MonoBehaviour, IDamagableHealth
	{
        public float GetHealth() { return health; }

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
        private void GetDamage(DamageInfo dmgInfo)
        {
            float pDmg = dmgInfo.dmg > health ? health : dmgInfo.dmg;

            dmgMaster.CallEventReceivedDamage(dmgInfo.origin, pDmg);

            health -= pDmg;

            if (health > 0)
                return;

            dmgMaster.CallEventDestroyObject(dmgInfo.origin);
            health = dmgMaster.GetDamagableSettings().health;
        }
    }
}
