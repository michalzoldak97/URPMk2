using UnityEngine;

namespace URPMk2
{
    public class DamageInfo
    {
        public float dmg, pen;
        public DamageType damageType;
        public Transform toDmg;
        public RaycastHit hit;

        public DamageInfo(DamageType damageType)
        {
            this.damageType = damageType;
        }
    }
}
