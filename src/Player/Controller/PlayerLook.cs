using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform fpsCamera;
        private float clampDeg, xRot;
        private float[] sensitivityXY;
        private Vector3 up = Vector3.up;
        private Vector3 left = Vector3.left;
        private Transform myTransform;
        private InputAction lookX;
        private InputAction lookY;
        private void SetInit()
        {
            PlayerMoveSettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerMoveSettings;
            clampDeg = playerSettings.lookClamp;
            sensitivityXY = playerSettings.lookSensitivity;
            myTransform = gameObject.transform;
            lookX = InputManager.playerInputActions.Humanoid.MouseX;
            lookY = InputManager.playerInputActions.Humanoid.MouseY;
        }
        private void OnEnable()
        {
            SetInit();
            lookX.Enable();
            lookY.Enable();
        }
        private void OnDisable()
        {
            lookX.Disable();
            lookY.Disable();
        }
        private void LookHorizontal(float mouseX)
        {
            myTransform.Rotate(up, mouseX * sensitivityXY[0]);
        }
        private void LookVertical(float mouseY)
        {
            xRot += mouseY * sensitivityXY[1];
            xRot = Mathf.Clamp(xRot, -clampDeg, clampDeg);
            fpsCamera.localEulerAngles = left * xRot;
        }
        private void Look()
        {
            float mouseX = lookX.ReadValue<float>();
            float mouseY = lookY.ReadValue<float>();
            if (mouseX != 0.0f)
                LookHorizontal(mouseX);
            if (mouseY != 0.0f)
                LookVertical(mouseY);
        }
        private void Update()
        {
            Look();
        }
    }
}
