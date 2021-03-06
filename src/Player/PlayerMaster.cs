using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class PlayerMaster : MonoBehaviour, IAmmoMaster
    {
        [SerializeField] private PlayerSettings playerSettings;
        public PlayerSettings GetPlayerSettings() { return playerSettings; }
        public delegate void PlayerAmmoEventsHandler(string ammoCode, int amount, WeaponAmmo origin);
        public event PlayerAmmoEventsHandler EventPlayerAmmoChange;
     
        public void CallEventAmmoChange(string ammoCode, int amount, WeaponAmmo origin)
        {
            EventPlayerAmmoChange?.Invoke(ammoCode, amount, origin);
        }
        public Dictionary<string, int> GetAmmoStore()
        {
            return GetComponent<PlayerAmmo>().playerAmmoStore;
        }
    }
}