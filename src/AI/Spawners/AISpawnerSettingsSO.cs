using UnityEngine;

namespace URPMk2
{
    public class AISpawnerSettingsSO : ScriptableObject
    {
        public int maxSquads;
        public int squadCountThreshold;
        public float spawnRadius;
        public float squadCheckPeriod;
        public float[] singleSpawnFreqRange; //min max
        public AISquadType[] squad;
    }
}