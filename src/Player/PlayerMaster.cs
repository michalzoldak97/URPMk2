using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class PlayerMaster : MonoBehaviour
    {
        public bool isWalking;
        private void Start()
        {
            InputManager.Start();
        }
    }
}