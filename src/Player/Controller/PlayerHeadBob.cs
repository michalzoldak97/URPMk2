using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class PlayerHeadBob : MonoBehaviour
    {
        [SerializeField] private Transform _fpsCamera;
        private float _bobTimer;
        private float[] _cameraYXPos;
        private float[] _headBobSpeed;
        private float[] _headBobMagnitude;
        private float[] _verHorBobMultiplayer;
        private Vector3 _cameraPosToSet = Vector3.zero;
        private FPSMovementEventsHandler _eventsHandler;
        private void SetInit()
        {
            _eventsHandler = GetComponent<FPSMovementEventsHandler>();
            PlayerMoveSettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerMoveSettings;
            _headBobSpeed = playerSettings.headBobSpeed;
            _headBobMagnitude = playerSettings.headBobMagnitude;
            _verHorBobMultiplayer = playerSettings.verHorBobMultiplayer;
            _cameraYXPos = new float[] { _fpsCamera.localPosition.y, _fpsCamera.localPosition.x };
        }
        private void OnEnable()
        {
            SetInit();
            _eventsHandler.EventStep += HandleHeadBob;
            _eventsHandler.EventStoppedMoving += ResetDefaultCaameraPos;
        }
        private void OnDisable()
        {
            _eventsHandler.EventStep -= HandleHeadBob;
            _eventsHandler.EventStoppedMoving -= ResetDefaultCaameraPos;
        }
        private void HandleHeadBob(int speedIdx)
        {
            _bobTimer += Time.deltaTime * _headBobSpeed[speedIdx];
            float headShift = Mathf.Sin(_bobTimer) * _headBobMagnitude[speedIdx];
            _cameraPosToSet.x = _fpsCamera.localPosition.x + (headShift * _verHorBobMultiplayer[1]); _cameraPosToSet.y = _cameraYXPos[0] + (headShift * _verHorBobMultiplayer[0]); _cameraPosToSet.z =  _fpsCamera.localPosition.z;
            _fpsCamera.localPosition = _cameraPosToSet;
        }
        private void ResetDefaultCaameraPos(int dummy)
        {
            _cameraPosToSet.x = _cameraYXPos[1]; _cameraPosToSet.y = _cameraYXPos[0]; _cameraPosToSet.z = _fpsCamera.localPosition.z;
            _fpsCamera.localPosition = _cameraPosToSet;
        }
    }
}