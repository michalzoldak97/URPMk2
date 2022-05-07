using UnityEngine;

namespace URPMk2
{
    public static class GameConfig
    {
        public static float secToDestroy = 11;
        public static float secToCheckForPoolObj = 0.1f;
        public static float secEffectAlive = 10;
        public static string shootKey = "shoot";
        public static string reloadKey = "reload";
        public static LayerMask stoneLayers = LayerMask.GetMask("Default");
        public static LayerMask metalLayers = LayerMask.GetMask("Item", "Metal");
        public static WaitForSeconds waitEffectAlive = new WaitForSeconds(10); // should be set from SO
    }
}
