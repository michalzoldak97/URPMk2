using UnityEngine;

namespace URPMk2
{
    public class DamageInfo
    {
        public float dmg, pen;
        public DamageType damageType;
        public Transform toDmg, origin;
        public RaycastHit hit;

        public DamageInfo(DamageType damageType, Transform origin)
        {
            this.damageType = damageType;
            this.origin = origin;
        }
    }
}
