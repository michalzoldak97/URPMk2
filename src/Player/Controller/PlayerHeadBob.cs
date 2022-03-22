using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class PlayerHeadBob : MonoBehaviour
    {
        [SerializeField] private Transform fpsCamera;
        private float bobTimer;
        private float[] cameraYXPos;
        private float[] headBobSpeed;
        private float[] headBobMagnitude;
        private float[] verHorBobMultiplayer;
        private Vector3 cameraPosToSet = Vector3.zero;
        private FPSMovementEventsHandler eventsHandler;
        private void SetInit()
        {
            eventsHandler = GetComponent<FPSMovementEventsHandler>();
            PlayerMoveSettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerMoveSettings;
            headBobSpeed = playerSettings.headBobSpeed;
            headBobMagnitude = playerSettings.headBobMagnitude;
            verHorBobMultiplayer = playerSettings.verHorBobMultiplayer;
            cameraYXPos = new float[] { fpsCamera.localPosition.y, fpsCamera.localPosition.x };
        }
        private void OnEnable()
        {
            SetInit();
            eventsHandler.EventStep += HandleHeadBob;
            eventsHandler.EventStoppedMoving += ResetDefaultCaameraPos;
        }
        private void OnDisable()
        {
            eventsHandler.EventStep -= HandleHeadBob;
            eventsHandler.EventStoppedMoving -= ResetDefaultCaameraPos;
        }
        private void HandleHeadBob(int speedIdx)
        {
            bobTimer += Time.deltaTime * headBobSpeed[speedIdx];
            float headShift = Mathf.Sin(bobTimer) * headBobMagnitude[speedIdx];
            cameraPosToSet.x = fpsCamera.localPosition.x + (headShift * verHorBobMultiplayer[1]); cameraPosToSet.y = cameraYXPos[0] + 
                (headShift * verHorBobMultiplayer[0]); cameraPosToSet.z =  fpsCamera.localPosition.z;
            fpsCamera.localPosition = cameraPosToSet;
        }
        private void ResetDefaultCaameraPos(int dummy)
        {
            cameraPosToSet.x = cameraYXPos[1]; cameraPosToSet.y = cameraYXPos[0]; cameraPosToSet.z = fpsCamera.localPosition.z;
            fpsCamera.localPosition = cameraPosToSet;
        }
    }
}