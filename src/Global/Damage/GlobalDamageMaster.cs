using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public static class GlobalDamageMaster
    {
        private static Dictionary<Transform, IDamagableMaster> damagableObjects = 
            new Dictionary<Transform, IDamagableMaster>();

        public static void RegisterDamagable(Transform toRegister)
        {
            if (!damagableObjects.ContainsKey(toRegister)
                && toRegister.GetComponent<IDamagableMaster>() != null)
                damagableObjects.Add(toRegister, toRegister.GetComponent<IDamagableMaster>());
        }
        public static void UnregisterDamagable(Transform toRemove)
        {
            if (damagableObjects.ContainsKey(toRemove))
                damagableObjects.Remove(toRemove);
        }
        public static void DamageObjByGun(Transform toDmg, DamageType dmgType, float dmg, float pen)
        {
            if (damagableObjects.ContainsKey(toDmg))
                damagableObjects[toDmg].CallEventHitByGun(dmg, pen);
        }
        public static void DamageObjByExplosion(Transform toDmg, DamageType dmgType, float dmg, float pen)
        {
            if (damagableObjects.ContainsKey(toDmg))
                damagableObjects[toDmg].CallEventHitByExplosion(dmg, pen);
        }
    }
}