using UnityEngine;

namespace URPMk2
{
    public static class GameConfig
    {
        public static float keepEffectsAliveSec = 10;
        public static string shootKey = "shoot";
        public static string reloadKey = "reload";
        public static LayerMask stoneLayers = LayerMask.GetMask("Default");
        public static LayerMask metalLayers = LayerMask.GetMask("Item", "Metal");
    }
}
