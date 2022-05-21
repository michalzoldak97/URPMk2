using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class DestructibleEffect : MonoBehaviour
	{
		[SerializeField] private DestructibleSettingsSO settings;
		[SerializeField] private AudioClip sound;
		private Transform myTransform;
		private Rigidbody[] shrads;
		private void SetInit()
		{
			if (myTransform != null &&
				shrads != null)
            {
				if (settings.isExplosion)
					Explode();
				StartCoroutine(Deactivate());
				return;
			}
			myTransform = transform;
			shrads = GetComponentsInChildren<Rigidbody>();
		}
        private IEnumerator Deactivate()
        {
			yield return GameConfig.waitEffectAlive;
			Destroy(gameObject, GameConfig.secEffectAlive);
			gameObject.SetActive(false);
        }
		private void Explode()
		{
			myTransform.Rotate(Utils.GetVector3FromFloat(settings.rotVec));
			foreach (Rigidbody rb in shrads)
            {
				rb.AddExplosionForce(
					settings.explosionForce, 
					myTransform.position,
					settings.explosionRadius, 
					1, 
					ForceMode.Impulse);
            }
			AudioSource.PlayClipAtPoint(sound, myTransform.position);
		}

		private void OnEnable()
		{
			SetInit();
		}
	}
}
