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
        public static void DamageObj(DamageInfo dmgInfo)
        {
            if (!damagableObjects.ContainsKey(dmgInfo.toDmg))
                return;

            switch (dmgInfo.damageType)
            {
                case DamageType.Gun:
                    if (dmgInfo.pen > damagableObjects[dmgInfo.toDmg].GetArmor())
                        damagableObjects[dmgInfo.toDmg].CallEventHitByGun(dmgInfo);
                    break;
                case DamageType.Explosion:
                    damagableObjects[dmgInfo.toDmg].CallEventHitByExplosion(dmgInfo);
                    break;
            }
        }
    }
}