using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class InputManager : MonoBehaviour
    {
        public static PlayerInputActions playerInputActions = new PlayerInputActions();
        public static event Action<InputActionMap> actionMapChange;

        private void Start()
        {
            ToggleActionMap(playerInputActions.Humanoid);
        }

        public static void ToggleActionMap(InputActionMap actionMapToSet)
        {
            if (actionMapToSet.enabled)
                return;

            playerInputActions.Disable();
            actionMapChange?.Invoke(actionMapToSet);
            actionMapToSet.Enable();
        }
    }
}
