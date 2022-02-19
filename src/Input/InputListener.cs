using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class InputListener : MonoBehaviour
    {
        protected PlayerInputActions inputActions;

        private void Awake()
        {
            inputActions = InputManager.playerInputActions;
        }
    }
}
