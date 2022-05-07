using System.Collections;
using UnityEngine;
namespace URPMk2
{
    public class GunHitEffect : MonoBehaviour
    {
        [SerializeField] AudioClip myHitSound;
        private AudioSource myAudioSource;
        private void Start()
        {
            myAudioSource = GetComponent<AudioSource>();
        }
        private void OnEnable()
        {
            PlayHitSound();
        }
        private void PlayHitSound()
        {
            if (myAudioSource == null)
                myAudioSource = GetComponent<AudioSource>();

            myAudioSource.PlayOneShot(myHitSound);
        }
    }
}
