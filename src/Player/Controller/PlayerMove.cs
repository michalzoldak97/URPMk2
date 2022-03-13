using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class PlayerMove : MonoBehaviour
    {
        private bool _isRunning;
        private float _walkSpeed, _runSpeed, _jumpSpeed, _runStepLenght, _characterRadius, _gravityMultiplayer, _inertiaCoeff;
        private Vector3 _upDir = Vector3.up;
        private Vector3 _moveDir = Vector3.zero;
        private Vector3 _gravity = Physics.gravity;
        private Transform _myTransform;
        private CharacterController _myCharacterController;
        private PlayerMaster _playerMaster;
        private InputAction _move;
        private void SetInit()
        {
            _myTransform = gameObject.transform;
            _myCharacterController = GetComponent<CharacterController>();
            _characterRadius = _myCharacterController.radius;
            _playerMaster = GetComponent<PlayerMaster>();
            PlayerMoveSettings playerSettings = _playerMaster.GetPlayerSettings().playerMoveSettings;
            _walkSpeed = playerSettings.walkSpeed;
            _runSpeed = playerSettings.runSpeed;
            _jumpSpeed = playerSettings.jumpSpeed;
            _gravityMultiplayer = playerSettings.gravityMultiplayer;
            _inertiaCoeff = playerSettings.inertiaCoefficient;
            _move = InputManager.playerInputActions.Humanoid.Move;
        }
        private void OnEnable()
        {
            SetInit();
            _move.Enable();

            InputManager.playerInputActions.Humanoid.Jump.performed += HandleJump;
            InputManager.playerInputActions.Humanoid.Jump.Enable();
        }
        private void OnDisable()
        {
            _move.Disable();
            InputManager.playerInputActions.Humanoid.Jump.Disable();
        }
        private void HandleJump(InputAction.CallbackContext obj)
        {
            if (!_myCharacterController.isGrounded)
                return;
            _moveDir.y = _jumpSpeed;
        }
        private void CalcMovVecXZ(Vector2 moveVector)
        {
            bool areControlsPressed = !(moveVector.x == 0f && moveVector.y == 0f);
            if (areControlsPressed && _myCharacterController.isGrounded)
            {
                float speed = _isRunning ? _walkSpeed : _runSpeed;
                Vector3 moveDir = _myTransform.forward * moveVector.y + _myTransform.right * moveVector.x;
                Physics.SphereCast(_myTransform.position, _characterRadius, -_upDir, out RaycastHit hitInfo,
                                   _myCharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                moveDir = Vector3.ProjectOnPlane(moveDir, hitInfo.normal).normalized;
                _moveDir.x = moveDir.x * speed;
                _moveDir.z = moveDir.z * speed;
            }
            else if (areControlsPressed)
            {
                _moveDir.x = _moveDir.x * _inertiaCoeff;
                _moveDir.z = _moveDir.z * _inertiaCoeff;
            }
            else
            {
                _moveDir.x = 0f;
                _moveDir.z = 0f;
            }
        }
        private void StickPlayerToTheGround()
        {
            if (!_myCharacterController.isGrounded)
                _moveDir += _gravityMultiplayer * Time.fixedDeltaTime * _gravity;
        }
        private void MovePlayer(Vector2 moveVector)
        {
            CalcMovVecXZ(moveVector);
            Debug.Log("Move dir = " + _moveDir);
            _myCharacterController.Move(_moveDir * Time.fixedDeltaTime);
            StickPlayerToTheGround();
        }
        private void FixedUpdate()
        {
            MovePlayer(_move.ReadValue<Vector2>());
        }
    }
}
