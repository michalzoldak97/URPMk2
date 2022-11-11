using UnityEngine;

namespace URPMk2
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DamagableSettings", order = 4)]
    public class DamagableSettingsSO : ScriptableObject
    {
        public int armor;
        public float health = 100f;
        public float lowHealth = 10f;
        public GameObject[] destructionEffects;
    }
}
