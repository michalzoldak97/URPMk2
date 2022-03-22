using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
    public class PlayerMoveSounds : MonoBehaviour
    {
        [SerializeField] private Transform fpsCamera;
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip landSound;
        [SerializeField] private AudioClip[] stoneStepSounds;
        [SerializeField] private AudioClip[] metalStepSounds;
        [SerializeField] private AudioClip[] grassStepSounds;
        private float stepTimer;
        private float[] stepSpeed;
        private string defaultTag = "Untagged";
        private string[] stoneSurfaceTags;
        private string[] metalSurfaceTags;
        private string[] grassSurfaceTags;
        private Vector3 down = Vector3.down;
        private FPSMovementEventsHandler eventsHandler;
        private void SetInit()
        {
            eventsHandler = GetComponent<FPSMovementEventsHandler>();
            PlayerMoveSettings playerSettings = GetComponent<PlayerMaster>().GetPlayerSettings().playerMoveSettings;
            stepSpeed = playerSettings.stepSpeed;
            stoneSurfaceTags = playerSettings.stoneSurfaceTags;
            metalSurfaceTags = playerSettings.metalSurfaceTags;
            grassSurfaceTags = playerSettings.grassSurfaceTags;
        }
        private void OnEnable()
        {
            SetInit();
            eventsHandler.EventStep += PlayFootStepSound;
            eventsHandler.EventLand += PlayLandSound;
            InputManager.playerInputActions.Humanoid.Jump.performed += PlayJumpSound;
        }
        private void OnDisable()
        {
            eventsHandler.EventStep -= PlayFootStepSound;
            InputManager.playerInputActions.Humanoid.Jump.performed -= PlayJumpSound;
            eventsHandler.EventLand -= PlayLandSound;
        }
        private void PlayJumpSound(InputAction.CallbackContext obj)
        {
            playerAudioSource.PlayOneShot(jumpSound);
        }
        private void PlayLandSound(int dummy)
        {
            playerAudioSource.PlayOneShot(landSound);
        }
        private void PlayFootStepSound(int stateIdx)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0)
            {
                if(Physics.Raycast(fpsCamera.position, down, out RaycastHit hit, 3))
                {
                    string surfaceTag = hit.collider.tag;
                    if (surfaceTag == defaultTag)
                    {
                        playerAudioSource.PlayOneShot(stoneStepSounds[UnityEngine.Random.Range(0, stoneStepSounds.Length - 1)]);
                    }
                    else if (Array.Exists(stoneSurfaceTags, el => el == surfaceTag))
                    {
                        playerAudioSource.PlayOneShot(stoneStepSounds[UnityEngine.Random.Range(0, stoneStepSounds.Length - 1)]);
                    }
                    else if (Array.Exists(metalSurfaceTags, el => el == surfaceTag))
                    {
                        playerAudioSource.PlayOneShot(metalStepSounds[UnityEngine.Random.Range(0, metalStepSounds.Length - 1)]);
                    }
                    else if (Array.Exists(grassSurfaceTags, el => el == surfaceTag))
                    {
                        playerAudioSource.PlayOneShot(grassStepSounds[UnityEngine.Random.Range(0, grassStepSounds.Length - 1)]);
                    }
                    else
                    {
                        playerAudioSource.PlayOneShot(stoneStepSounds[UnityEngine.Random.Range(0, stoneStepSounds.Length - 1)]);
                    }
                }
                stepTimer = stepSpeed[stateIdx];
            }
        }
    }
}
