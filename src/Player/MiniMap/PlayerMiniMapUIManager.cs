using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class PlayerMiniMapUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject mipMapUIParent;
        [SerializeField] private Transform miniMapCamera;
        private bool isMiniMapActive, isCameraMoving;
        private bool[] cameraMoveDirections;
        private PlayerMiniMapSettings playerMiniMapSettings;
        private PlayerMaster playerMaster;
        private void SetInit()
        {
            playerMaster = GetComponent<PlayerMaster>();
            playerMiniMapSettings = playerMaster.GetPlayerSettings().playerMiniMapSettings;
            cameraMoveDirections = new bool[6];
        }
        private void OnEnable()
        {
            SetInit();
            InputManager.playerInputActions.Humanoid.ToggleMiniMap.performed += EnableMiniMapUI;
            InputManager.playerInputActions.Humanoid.ToggleMiniMap.Enable();
            InputManager.playerInputActions.UI.ToggleMiniMap.performed += DisableMiniMapUI;
            InputManager.playerInputActions.UI.ToggleMiniMap.Enable();
        }
        private void OnDisable()
        {
            InputManager.playerInputActions.Humanoid.ToggleMiniMap.performed -= EnableMiniMapUI;
            InputManager.playerInputActions.Humanoid.ToggleMiniMap.Disable();
            InputManager.playerInputActions.UI.ToggleMiniMap.performed -= DisableMiniMapUI;
            InputManager.playerInputActions.UI.ToggleMiniMap.Disable();
        }
        public void StopCameraMove()
        {
            isCameraMoving = false;
            for (int i = 0; i < cameraMoveDirections.Length; i++)
            {
                cameraMoveDirections[i] = false;
            }
            StopAllCoroutines();
        }
        private IEnumerator MoveMiniMapCameraActions()
        {
            Vector3 mv = Vector3.zero;
            while (isCameraMoving)
            {
                if (cameraMoveDirections[0] && // r
                miniMapCamera.position.x + playerMiniMapSettings.cameraMoveSpeed <= 
                playerMiniMapSettings.maxXYZcameraPos[0])
                    mv.x = playerMiniMapSettings.cameraMoveSpeed;
                
                else if (cameraMoveDirections[1] && // l
                miniMapCamera.position.x - playerMiniMapSettings.cameraMoveSpeed >= 0)
                    mv.x = -playerMiniMapSettings.cameraMoveSpeed;

                else if (cameraMoveDirections[2] &&
                miniMapCamera.position.z + playerMiniMapSettings.cameraMoveSpeed >= 0)
                    mv.z = playerMiniMapSettings.cameraMoveSpeed;

                else if (cameraMoveDirections[3] &&
                miniMapCamera.position.z - playerMiniMapSettings.cameraMoveSpeed <=
                playerMiniMapSettings.maxXYZcameraPos[2])
                    mv.z = -playerMiniMapSettings.cameraMoveSpeed;

                else if (cameraMoveDirections[4] && 
                miniMapCamera.position.y + playerMiniMapSettings.cameraMoveSpeed <=
                playerMiniMapSettings.maxXYZcameraPos[1])
                    mv.y = playerMiniMapSettings.cameraMoveSpeed;

                else if (cameraMoveDirections[5] &&
                miniMapCamera.position.y - playerMiniMapSettings.cameraMoveSpeed >= 5)
                    mv.y = -playerMiniMapSettings.cameraMoveSpeed;

                miniMapCamera.position += mv;
                mv.x = 0; mv.y = 0; mv.z = 0;
                yield return Utils.waitForFixedUpdate;
            }
        }
        public void MoveMiniMapCamera(int dir)
        {
            for (int i = 0; i < cameraMoveDirections.Length; i++)
            {
                cameraMoveDirections[i] = false;
            }
            cameraMoveDirections[dir] = true;
            isCameraMoving = true;
            StartCoroutine(MoveMiniMapCameraActions());
        }
        private void ToggleMiniMapUI()
        {
            
            CursorManager.ToggleCursorState(isMiniMapActive);
            if (isMiniMapActive)
                InputManager.ToggleActionMap(InputManager.playerInputActions.UI);
            else
                InputManager.ToggleActionMap(InputManager.playerInputActions.Humanoid);
            if (isMiniMapActive)
            {
                InputManager.playerInputActions.UI.Enable();
                InputManager.playerInputActions.Humanoid.Disable();
            }
            else
            {
                InputManager.playerInputActions.UI.Disable();
                InputManager.playerInputActions.Humanoid.Enable();
            }
            miniMapCamera.gameObject.SetActive(isMiniMapActive);
            mipMapUIParent.SetActive(isMiniMapActive);
        }
        private void EnableMiniMapUI(InputAction.CallbackContext obj)
        {
            isMiniMapActive = !isMiniMapActive;
			ToggleMiniMapUI();
        }
        private void DisableMiniMapUI(InputAction.CallbackContext obj)
        {
            isMiniMapActive = !isMiniMapActive;
			ToggleMiniMapUI();
        }
    }
}
