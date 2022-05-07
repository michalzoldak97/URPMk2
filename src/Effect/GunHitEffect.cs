using System.Collections;
using UnityEngine;
namespace URPMk2
{
    public class GunHitEffect : MonoBehaviour, IPooledObject
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
        private void OnDisable()
        {
            transform.localScale.Set(1, 1, 1);
        }
        public void ResetObjectState()
        {
            StopAllCoroutines();
            StartCoroutine(DeactivateEffect());
        }
        private void PlayHitSound()
        {
            if (myAudioSource == null)
                myAudioSource = GetComponent<AudioSource>();

            myAudioSource.PlayOneShot(myHitSound);
        }
        private IEnumerator DeactivateEffect()
        {
            yield return GameConfig.waitEffectAlive;
            transform.SetParent(null);
            gameObject.SetActive(false);
        }
    }
}
