using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        public PlayerMoveSettings playerMoveSettings;
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
    }
}
