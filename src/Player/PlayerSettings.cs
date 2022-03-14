using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        public PlayerMoveSettings playerMoveSettings;
        public PlayerInventorySettings playerInventorySettings;
    }
    [System.Serializable]
    public class PlayerInventorySettings
    {
        public int labelFontSize;
        public float itemCheckRate;
        public float[] itemLabelWidthHeight;
    }
    [System.Serializable]
    public class PlayerMoveSettings
    {
        public float walkSpeed;
        public float runSpeed;
        public float jumpSpeed;
        public float stickToGroundForce;
        public float gravityMultiplayer;
        public float inertiaCoefficient;
        public float[] lookSensitivity; // x, y
        public float[] headBobSpeed; // walk run
        public float[] headBobMagnitude; // walk run
        public float[] verHorBobMultiplayer;
        public float[] stepSpeed;
        public float lookClamp;
        public string[] stoneSurfaceTags;
        public string[] metalSurfaceTags;
        public string[] grassSurfaceTags;
    }
}
