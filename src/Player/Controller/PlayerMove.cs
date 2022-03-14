using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class PlayerMove : MonoBehaviour
    {
        private bool _isMoving;
        private int _speedIdx = 0;
        private float _walkSpeed, _runSpeed, _jumpSpeed, _characterRadius, _gravityMultiplayer, _inertiaCoeff;
        private float[] _speedVec;
        private Vector3 _upDir = Vector3.up;
        private Vector3 _moveDir = Vector3.zero;
        private Vector3 _gravity = Physics.gravity;
        private Transform _myTransform;
        private CharacterController _myCharacterController;
        private InputAction _move;
        private FPSMovementEventsHandler _movementEventsHandler;
        private void SetInit()
        {
            _myTransform = gameObject.transform;
            _myCharacterController = GetComponent<CharacterController>();
            _characterRadius = _myCharacterController.radius;
            PlayerMoveSettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerMoveSettings;
            _walkSpeed = playerSettings.walkSpeed;
            _runSpeed = playerSettings.runSpeed;
            _jumpSpeed = playerSettings.jumpSpeed;
            _speedVec = new float[] { _walkSpeed, _runSpeed, _jumpSpeed };
            _gravityMultiplayer = playerSettings.gravityMultiplayer;
            _inertiaCoeff = playerSettings.inertiaCoefficient;
            _move = InputManager.playerInputActions.Humanoid.Move;
            _movementEventsHandler = GetComponent<FPSMovementEventsHandler>();
        }
        private void OnEnable()
        {
            SetInit();
            _move.Enable();

            InputManager.playerInputActions.Humanoid.Jump.performed += HandleJump;
            InputManager.playerInputActions.Humanoid.Jump.Enable();

            InputManager.playerInputActions.Humanoid.RunStart.performed += StartRun;
            InputManager.playerInputActions.Humanoid.RunStart.Enable();

            InputManager.playerInputActions.Humanoid.RunFinish.performed += EndRun;
            InputManager.playerInputActions.Humanoid.RunFinish.Enable();
        }
        private void OnDisable()
        {
            _move.Disable();
            InputManager.playerInputActions.Humanoid.Jump.Disable();
            InputManager.playerInputActions.Humanoid.RunStart.Disable();
            InputManager.playerInputActions.Humanoid.RunFinish.Disable();
        }
        public void SetMoveSpeed(float[] toSet) 
        {
            _speedVec = toSet;
        }
        public float[] GetMoveSpeed()
        {
            return _speedVec;
        }
        private void StartRun(InputAction.CallbackContext obj)
        {
            _speedIdx = 1;
        }
        private void EndRun(InputAction.CallbackContext obj)
        {
            _speedIdx = 0;
        }
        private IEnumerator OverseeJumpState()
        {
            yield return new WaitForFixedUpdate();
            WaitUntil waitUntillLand = new WaitUntil(() => _myCharacterController.isGrounded);
            yield return waitUntillLand;
            _movementEventsHandler.CallEventLand(_speedIdx);
        }
        private void HandleJump(InputAction.CallbackContext obj)
        {
            if (!_myCharacterController.isGrounded)
                return;
            _moveDir.y = _speedVec[2];
            _movementEventsHandler.CallEventJump(_speedIdx);
            StartCoroutine(OverseeJumpState());
        }
        private void CalcMovVecXZ(Vector2 moveVector)
        {
            bool areControlsPressed = !(moveVector.x == 0f && moveVector.y == 0f);
            if (areControlsPressed && _myCharacterController.isGrounded)
            {
                Vector3 moveDir = _myTransform.forward * moveVector.y + _myTransform.right * moveVector.x;
                Physics.SphereCast(_myTransform.position, _characterRadius, -_upDir, out RaycastHit hitInfo,
                                   _myCharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                moveDir = Vector3.ProjectOnPlane(moveDir, hitInfo.normal).normalized;
                _moveDir.x = moveDir.x * _speedVec[_speedIdx];
                _moveDir.z = moveDir.z * _speedVec[_speedIdx];

                _movementEventsHandler.CallEventStep(_speedIdx);
                _isMoving = true;
            }
            else if (areControlsPressed)
            {
                _moveDir.x *= _inertiaCoeff;
                _moveDir.z *= _inertiaCoeff;
            }
            else
            {
                _moveDir.x = 0f;
                _moveDir.z = 0f;
                if (_isMoving)
                {
                    _movementEventsHandler.CallEventStoppedMoving(_speedIdx);
                    _isMoving = false;
                }
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
            _myCharacterController.Move(_moveDir * Time.fixedDeltaTime);
            StickPlayerToTheGround();
        }
        private void FixedUpdate()
        {
            MovePlayer(_move.ReadValue<Vector2>());
        }
    }
}
