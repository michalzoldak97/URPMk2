using UnityEngine;

namespace URPMk2
{
    public class DamagableSettingsSO : ScriptableObject
    {
        public int armor;
        public float health = 100;
        public GameObject[] destructionEffects;
    }
}
