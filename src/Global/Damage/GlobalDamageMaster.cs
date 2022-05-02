using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public static class GlobalDamageMaster
    {
        private static Dictionary<Transform, IDamagableMaster> damagableObjects = 
            new Dictionary<Transform, IDamagableMaster>();

        public static void RegisterDamagable(Transform k, IDamagableMaster v)
        {
            if (!damagableObjects.ContainsKey(k))
                damagableObjects.Add(k, v);
        }
        public static void UnregisterDamagable(Transform toRemove)
        {
            if (damagableObjects.ContainsKey(toRemove))
                damagableObjects.Remove(toRemove);
        }
        public static void DamageObj(Transform toDmg, DamageType dmgType, float dmg, float pen)
        {
            if (!damagableObjects.ContainsKey(toDmg))
                return;

            switch (dmgType)
            {
                case DamageType.Gun:
                    if (pen > damagableObjects[toDmg].GetArmor())
                        damagableObjects[toDmg].CallEventHitByGun(dmg, pen);
                    break;
                case DamageType.Explosion:
                    damagableObjects[toDmg].CallEventHitByExplosion(dmg, pen);
                    break;
            }
        }
    }
}