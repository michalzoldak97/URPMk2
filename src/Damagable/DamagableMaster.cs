using System.Collections;
using UnityEngine;

namespace URPMk2
{
    public class DamagableMaster : MonoBehaviour, IDamagableMaster
    {
        [SerializeField] private DamagableSettingsSO damagableSettings;
        public DamagableSettingsSO GetDamagableSettings() { return damagableSettings; }
        public int GetArmor() { return damagableSettings.armor; }
        public float GetHealth() { return dmgHealth.GetHealth(); }

        public delegate void DamageEventsHandler(DamageInfo dmgInfo);
        public event DamageEventsHandler EventHitByGun;
        public event DamageEventsHandler EventHitByExplosion;
        
        public delegate void DamagableEventsHandler();
        public event DamagableEventsHandler EventDestroyObject;
        public event DamagableEventsHandler EventHealthLow;
        public event DamagableEventsHandler EventHealthRecovered;

        public delegate void DamagableInformEventshandler(float dmg);
        public event DamagableInformEventshandler EventReceivedDamage;

        private bool receivedExplosionDmg;
        private DamagableHealth dmgHealth;
        private void Awake()
        {

            dmgHealth = GetComponent<DamagableHealth>();
        }
        private void Start()
        {
            // register obj in dictionary
            GlobalDamageMaster.RegisterDamagable(transform, this);
        }
        public void CallEventHitByGun(DamageInfo dmgInfo)
        {
            EventHitByGun?.Invoke(dmgInfo);
        }
        private IEnumerator ResetReceivedExplosion()
        {
            yield return Utils.waitForEndOfFrame;
            receivedExplosionDmg = false;
        }
        public void CallEventHitByExplosion(DamageInfo dmgInfo)
        {
            if (receivedExplosionDmg)
                return;
            else
            {
                receivedExplosionDmg = true;
                StartCoroutine(ResetReceivedExplosion());
            }
            EventHitByExplosion?.Invoke(dmgInfo);
        }
        public void CallEventDestroyObject()
        {
            EventDestroyObject?.Invoke();
        }
        public void CallEventReceivedDamage(float dmg)
        {
            EventReceivedDamage?.Invoke(dmg);
        }
        public void CallEventHealthLow()
        {
            EventHealthLow?.Invoke();
        }
        public void CallEventHealthRecovered()
        {
            EventHealthRecovered?.Invoke();
        }
    }
}