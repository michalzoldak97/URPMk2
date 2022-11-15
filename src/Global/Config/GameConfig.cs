using UnityEngine;

namespace URPMk2
{
    public static class GameConfig
    {
        public static float secToDestroy = 11;
        public static float secToCheckForPoolObj = 0.5f;
        public static float secEffectAlive = 10;
        public static float wanderTargetRandomRadius = 3.0f;
        public static float checkRateSetDelay = 0.7f;
        public static float maxSceneDistance = 360f;
        public static double refreshTeamsEverySec = 1;
        public static string shootKey = "shoot";
        public static string reloadKey = "reload";
        public static Vector3 maxMapDim = new Vector3(128f, 1f, 128f);
        public static LayerMask stoneLayers = LayerMask.GetMask("Default");
        public static LayerMask metalLayers = LayerMask.GetMask("Item", "Metal");
        public static WaitForSeconds waitEffectAlive = new WaitForSeconds(10); // should be set from SO
    }
}
