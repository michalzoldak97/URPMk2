using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace URPMk2
{
    public class PlayerMoveSounds : MonoBehaviour
    {
        [SerializeField] private Transform _fpsCamera;
        [SerializeField] private AudioSource _playerAudioSource;
        [SerializeField] private AudioClip[] _stoneStepSounds;
        [SerializeField] private AudioClip[] _metalStepSounds;
        [SerializeField] private AudioClip[] _grassStepSounds;
        private float _stepTimer;
        private float[] _stepSpeed;
        private string _defaultTag = "Untagged";
        private Vector3 _down = Vector3.down;
        private string[] _stoneSurfaceTags;
        private string[] _metalSurfaceTags;
        private string[] _grassSurfaceTags;
        private FPSMovementEventsHandler _eventsHandler;
        private void SetInit()
        {
            _eventsHandler = GetComponent<FPSMovementEventsHandler>();
            PlayerMoveSettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerMoveSettings;
            _stepSpeed = playerSettings.stepSpeed;
            _stoneSurfaceTags = playerSettings.stoneSurfaceTags;
            _metalSurfaceTags = playerSettings.metalSurfaceTags;
            _grassSurfaceTags = playerSettings.grassSurfaceTags;
        }
        private void OnEnable()
        {
            SetInit();
            _eventsHandler.EventStep += PlayFootStepSound;
        }
        private void OnDisable()
        {
            _eventsHandler.EventStep -= PlayFootStepSound;
        }
        
        private void PlayFootStepSound(int stateIdx)
        {
            _stepTimer -= Time.deltaTime;
            if (_stepTimer <= 0)
            {
                if(Physics.Raycast(_fpsCamera.position, _down, out RaycastHit hit, 3))
                {
                    string surfaceTag = hit.collider.tag;
                    if (surfaceTag == _defaultTag)
                    {
                        _playerAudioSource.PlayOneShot(_stoneStepSounds[UnityEngine.Random.Range(0, _stoneStepSounds.Length - 1)]);
                    }
                    else if (Array.Exists(_stoneSurfaceTags, el => el == surfaceTag))
                    {
                        _playerAudioSource.PlayOneShot(_stoneStepSounds[UnityEngine.Random.Range(0, _stoneStepSounds.Length - 1)]);
                    }
                    else if (Array.Exists(_metalSurfaceTags, el => el == surfaceTag))
                    {
                        _playerAudioSource.PlayOneShot(_metalStepSounds[UnityEngine.Random.Range(0, _metalStepSounds.Length - 1)]);
                    }
                    else if (Array.Exists(_grassSurfaceTags, el => el == surfaceTag))
                    {
                        _playerAudioSource.PlayOneShot(_grassStepSounds[UnityEngine.Random.Range(0, _grassStepSounds.Length - 1)]);
                    }
                    else
                    {
                        _playerAudioSource.PlayOneShot(_stoneStepSounds[UnityEngine.Random.Range(0, _stoneStepSounds.Length - 1)]);
                    }
                }
                _stepTimer = _stepSpeed[stateIdx];
            }
        }
    }
}
