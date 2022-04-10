using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class PlayerMaster : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        public PlayerSettings GetPlayerSettings() { return playerSettings; }
        public delegate void PlayerAmmoEventsHandler(string ammoCode, int amount);
        public event PlayerAmmoEventsHandler EventPlayerAmmoChange;
        private void Start()
        {
            InputManager.Start();
        }
        public void CallEventPlayerAmmoChange(string ammoCode, int amount)
        {
            EventPlayerAmmoChange?.Invoke(ammoCode, amount);
        }
    }
}