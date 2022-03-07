using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class PlayerMove : MonoBehaviour
    {
        private bool _isGroounded;
        private float _walkSpeed, _runSpeed, _jumpSpeed, _runStepLenght, _stickToGroundForce, _gravityMultiplayer;
        private Vector2 _previousMoveValue = Vector2.zero;
        private Transform _myTransform;
        private PlayerMaster _playerMaster;
        private InputAction _move;
        private void SetInit()
        {
            _playerMaster = GetComponent<PlayerMaster>();
            _move = InputManager.playerInputActions.Humanoid.Move;
            _myTransform = gameObject.transform;
        }
        private void OnEnable()
        {
            SetInit();
            _move.Enable();
        }
        private void OnDisable()
        {
            _move.Disable();
        }
        private void MovePlayer(Vector2 moveVector)
        {

        }
        private void FixedUpdate()
        {
            Debug.Log("Move values: " + _move.ReadValue<Vector2>());
        }
    }
}
