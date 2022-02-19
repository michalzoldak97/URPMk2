using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class ControllerHumanoid : InputListener
    {
        private InputAction move;
        private void SetInit()
        {
            move = inputActions.Humanoid.Move;
        }
        private void OnEnable()
        {
            SetInit();
            move.Enable();
        }
        private void OnDisable()
        {
            move.Disable();
        }
        private void FixedUpdate()
        {
            Debug.Log("Move values: " + move.ReadValue<Vector2>());
        }
    }
}
