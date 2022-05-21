using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class DestructibleEffect : MonoBehaviour
	{
		[SerializeField] private bool isExplosion;
		[SerializeField] private float explosionForce, explosionRadius;
		[SerializeField] private AudioSource audioSource;
		private Transform myTransform;
		private Rigidbody[] shrads;
		private void SetInit()
		{
			if (myTransform != null &&
				shrads != null)
				return;
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
			foreach (Rigidbody rb in shrads)
            {
				rb.AddExplosionForce(
					explosionForce, 
					myTransform.position, 
					explosionRadius, 
					1, 
					ForceMode.Impulse);
            }
		}

		private void OnEnable()
		{
			SetInit();
			Explode();
			StartCoroutine(Deactivate());
		}
	}
}
