using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class PlayerMaster : MonoBehaviour
    {
        public bool isWalking;
        [SerializeField] private PlayerSettings playerSettings;
        public PlayerSettings GetPlayerSettings() { return playerSettings; }
        private void Start()
        {
            InputManager.Start();
        }
    }
}