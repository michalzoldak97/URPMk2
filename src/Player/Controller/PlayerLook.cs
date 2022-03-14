using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform _fpsCamera;
        private float _clampDeg, _xRot;
        private float[] _sensitivityXY;
        private Vector3 _up = Vector3.up;
        private Vector3 _left = Vector3.left;
        private Transform _myTransform;
        private InputAction _lookX;
        private InputAction _lookY;
        private void SetInit()
        {
            PlayerMoveSettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerMoveSettings;
            _clampDeg = playerSettings.lookClamp;
            _sensitivityXY = playerSettings.lookSensitivity;
            _myTransform = gameObject.transform;
            _lookX = InputManager.playerInputActions.Humanoid.MouseX;
            _lookY = InputManager.playerInputActions.Humanoid.MouseY;
        }
        private void OnEnable()
        {
            SetInit();
            _lookX.Enable();
            _lookY.Enable();
        }
        private void OnDisable()
        {
            _lookX.Disable();
            _lookY.Disable();
        }
        private void LookHorizontal(float mouseX)
        {
            _myTransform.Rotate(_up, mouseX * _sensitivityXY[0]);
        }
        private void LookVertical(float mouseY)
        {
            _xRot += mouseY * _sensitivityXY[1];
            _xRot = Mathf.Clamp(_xRot, -_clampDeg, _clampDeg);
            _fpsCamera.localEulerAngles = _left * _xRot;
        }
        private void Look()
        {
            float mouseX = _lookX.ReadValue<float>();
            float mouseY = _lookY.ReadValue<float>();
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
