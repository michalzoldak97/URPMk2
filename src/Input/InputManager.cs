using System;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public static class InputManager
    {
        public static PlayerInputActions playerInputActions = new PlayerInputActions();
        public static event Action<InputActionMap> actionMapChange;

        public static void Start()
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
