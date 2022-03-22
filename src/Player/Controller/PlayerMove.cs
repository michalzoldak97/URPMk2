using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class PlayerMove : MonoBehaviour
    {
        private bool isMoving;
        private int speedIdx = 0;
        private float walkSpeed, runSpeed, jumpSpeed, characterRadius, gravityMultiplayer, inertiaCoeff;
        private float[] speedVec;
        private Vector3 upDir = Vector3.up;
        private Vector3 moveDir = Vector3.zero;
        private Vector3 gravity = Physics.gravity;
        private Transform myTransform;
        private CharacterController myCharacterController;
        private InputAction move;
        private FPSMovementEventsHandler movementEventsHandler;
        private void SetInit()
        {
            myTransform = gameObject.transform;
            myCharacterController = GetComponent<CharacterController>();
            characterRadius = myCharacterController.radius;
            PlayerMoveSettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerMoveSettings;
            walkSpeed = playerSettings.walkSpeed;
            runSpeed = playerSettings.runSpeed;
            jumpSpeed = playerSettings.jumpSpeed;
            speedVec = new float[] { walkSpeed, runSpeed, jumpSpeed };
            gravityMultiplayer = playerSettings.gravityMultiplayer;
            inertiaCoeff = playerSettings.inertiaCoefficient;
            move = InputManager.playerInputActions.Humanoid.Move;
            movementEventsHandler = GetComponent<FPSMovementEventsHandler>();
        }
        private void OnEnable()
        {
            SetInit();
            move.Enable();

            InputManager.playerInputActions.Humanoid.Jump.performed += HandleJump;
            InputManager.playerInputActions.Humanoid.Jump.Enable();

            InputManager.playerInputActions.Humanoid.RunStart.performed += StartRun;
            InputManager.playerInputActions.Humanoid.RunStart.Enable();

            InputManager.playerInputActions.Humanoid.RunFinish.performed += EndRun;
            InputManager.playerInputActions.Humanoid.RunFinish.Enable();
        }
        private void OnDisable()
        {
            move.Disable();
            InputManager.playerInputActions.Humanoid.Jump.performed -= HandleJump;
            InputManager.playerInputActions.Humanoid.Jump.Disable();
            InputManager.playerInputActions.Humanoid.RunStart.performed -= StartRun;
            InputManager.playerInputActions.Humanoid.RunStart.Disable();
            InputManager.playerInputActions.Humanoid.RunFinish.performed -= EndRun;
            InputManager.playerInputActions.Humanoid.RunFinish.Disable();
        }
        public void SetMoveSpeed(float[] toSet) 
        {
            speedVec = toSet;
        }
        public float[] GetMoveSpeed()
        {
            return speedVec;
        }
        private void StartRun(InputAction.CallbackContext obj)
        {
            speedIdx = 1;
        }
        private void EndRun(InputAction.CallbackContext obj)
        {
            speedIdx = 0;
        }
        private IEnumerator OverseeJumpState()
        {
            yield return new WaitForFixedUpdate();
            WaitUntil waitUntillLand = new WaitUntil(() => myCharacterController.isGrounded);
            yield return waitUntillLand;
            movementEventsHandler.CallEventLand(speedIdx);
        }
        private void HandleJump(InputAction.CallbackContext obj)
        {
            if (!myCharacterController.isGrounded)
                return;
            moveDir.y = speedVec[2];
            movementEventsHandler.CallEventJump(speedIdx);
            StartCoroutine(OverseeJumpState());
        }
        private void CalcMovVecXZ(Vector2 moveVector)
        {
            bool areControlsPressed = !(moveVector.x == 0f && moveVector.y == 0f);
            if (areControlsPressed && myCharacterController.isGrounded)
            {
                Vector3 mDir = myTransform.forward * moveVector.y + myTransform.right * moveVector.x;
                Physics.SphereCast(myTransform.position, characterRadius, -upDir, out RaycastHit hitInfo,
                                   myCharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                mDir = Vector3.ProjectOnPlane(mDir, hitInfo.normal).normalized;
                moveDir.x = mDir.x * speedVec[speedIdx];
                moveDir.z = mDir.z * speedVec[speedIdx];

                movementEventsHandler.CallEventStep(speedIdx);
                isMoving = true;
            }
            else if (areControlsPressed)
            {
                moveDir.x *= inertiaCoeff;
                moveDir.z *= inertiaCoeff;
            }
            else
            {
                moveDir.x = 0f;
                moveDir.z = 0f;
                if (isMoving)
                {
                    movementEventsHandler.CallEventStoppedMoving(speedIdx);
                    isMoving = false;
                }
            }
        }
        private void StickPlayerToTheGround()
        {
            if (!myCharacterController.isGrounded)
                moveDir += gravityMultiplayer * Time.fixedDeltaTime * gravity;
        }
        private void MovePlayer(Vector2 moveVector)
        {
            CalcMovVecXZ(moveVector);
            myCharacterController.Move(moveDir * Time.fixedDeltaTime);
            StickPlayerToTheGround();
        }
        private void FixedUpdate()
        {
            MovePlayer(move.ReadValue<Vector2>());
        }
    }
}
