using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public struct DamageObjectData
    {
        public string uID;
        public Teams objTeam;
        public float dmg;
    }
    public static class GlobalDamageMaster
    {
        public static Dictionary<string, DamageObjectData> dmgStatistics =
            new Dictionary<string, DamageObjectData>();

        private static readonly Dictionary<Transform, IDamagableMaster> damagableObjects = 
            new Dictionary<Transform, IDamagableMaster>();

        public delegate void GlobalDamageEventHandler(Transform origin);
        public static event GlobalDamageEventHandler EventRegisterDestruction;

        public static void CallEventRegisterDestruction(Transform origin)
        {
            EventRegisterDestruction?.Invoke(origin);
        }

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
        public static void RegisterDamage(Transform origin, float dmg)
        {
            string key = origin.name + origin.GetInstanceID();
            if (!dmgStatistics.ContainsKey(key))
            {
                DamageObjectData objData = new DamageObjectData();
                objData.uID = key;
                objData.dmg = dmg;
                objData.objTeam = origin.GetComponent<ITeamMember>().TeamID;
                dmgStatistics.Add(key, objData);
            }
            else
            {
                DamageObjectData objData = dmgStatistics[key];
                objData.dmg += dmg;
                dmgStatistics[key] = objData;
            }
        }
        public static float GetDamageForEntity(string key)
        {
            if (!dmgStatistics.ContainsKey(key))
                return 0f;

            return dmgStatistics[key].dmg;
        }
    }
}