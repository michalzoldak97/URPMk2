using UnityEngine;

namespace URPMk2
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CargoSettings", order = 10)]
    public class CargoSettingsSO : ScriptableObject
    {
        public int boardingCapacity;
        public int botsAliveThreshold;
        public float checkRate;
        public float checkSpawnInterval;
        public GameObject botObj;
    }
}
