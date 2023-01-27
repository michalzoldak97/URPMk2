using UnityEngine;

namespace URPMk2
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AISpawnerSettings", order = 9)]
    public class AISpawnerSettingsSO : ScriptableObject
    {
        public int agentsThreshold;
        public int maxSquads;
        public float spawnRadius;
        public float squadSpawnPeriod;
        public float[] singleSpawnFreqRange; //min max
        public float[] spawnPointOffset;
        public AISquadType[] squad;
    }
}