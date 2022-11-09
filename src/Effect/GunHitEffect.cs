using UnityEngine;
using UnityEngine.VFX;
namespace URPMk2
{
    public class GunHitEffect : MonoBehaviour, IPooledObject
    {
        [SerializeField] AudioClip myHitSound;
        private AudioSource myAudioSource;
        private VisualEffect effect;
        private void Start()
        {
            myAudioSource = GetComponent<AudioSource>();
            effect = GetComponent<VisualEffect>();
        }
        private void OnEnable()
        {
            PlayHitSound();
            PlayEffect();
        }
        private void PlayHitSound()
        {
            if (myAudioSource == null)
                myAudioSource = GetComponent<AudioSource>();

            myAudioSource.PlayOneShot(myHitSound);
        }
        private void PlayEffect()
        {
            if (effect == null)
                effect = gameObject.GetComponent<VisualEffect>();

            effect.Play();
        }
        public void Activate()
        {
            PlayHitSound();
            PlayEffect();
        }
        public void Activate(string dummy) {}
    }
}
