using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class DamagableMaster : MonoBehaviour, IDamagableMaster
    {
        [SerializeField] private DamagableSettingsSO damagableSettings;
        public DamagableSettingsSO GetDamagableSettings() { return damagableSettings; }

        public int GetArmor() { return damagableSettings.armor; }

        public delegate void DamageEventsHandler(DamageInfo dmgInfo);
        public event DamageEventsHandler EventHitByGun;
        public event DamageEventsHandler EventHitByExplosion;

        public delegate void DamagableEventsHandler();
        public event DamagableEventsHandler EventDestroyObject;

        public delegate void DamagableInformEventshandler(float dmg);
        public event DamagableInformEventshandler EventReceivedDamage;

        private void Start()
        {
            // register obj in dictionary
            GlobalDamageMaster.RegisterDamagable(transform, this);
        }
        public void CallEventHitByGun(DamageInfo dmgInfo)
        {
            EventHitByGun?.Invoke(dmgInfo);
        }
        public void CallEventHitByExplosion(DamageInfo dmgInfo)
        {
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
    }
}