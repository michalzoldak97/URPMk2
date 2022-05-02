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

        private void Start()
        {
            // register obj in dictionary
            GlobalDamageMaster.RegisterDamagable(transform, this);
        }
        public void CallEventHitByGun(float dmg, float pen)
        {
            Debug.Log("Hitten by gun");
        }
        public void CallEventHitByExplosion(float dmg, float pen)
        {

        }
    }
}