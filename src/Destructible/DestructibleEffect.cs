using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class DestructibleEffect : MonoBehaviour
	{
		[SerializeField] private AudioClip sound;
		private Transform myTransform;
		private Rigidbody[] shrads;
		private DestructibleMaster destructibleMaster;
		private void SetInit()
		{
			destructibleMaster = GetComponent<DestructibleMaster>();
			if (myTransform != null &&
				shrads != null)
            {
				if (destructibleMaster.GetDestructibleSettings().isExplosion)
					Explode();
				StartCoroutine(Deactivate());
				return;
			}
			myTransform = transform;
			shrads = GetComponentsInChildren<Rigidbody>();
			destructibleMaster.CallEventInitializeDestructibleEffect();
		}
        private IEnumerator Deactivate()
        {
			yield return GameConfig.waitEffectAlive;
			Destroy(gameObject, GameConfig.secEffectAlive + 1);
			destructibleMaster.CallEventDeactivateDestructibleEffect();
			gameObject.SetActive(false);
        }
		private void Explode()
		{
			DestructibleSettingsSO settings = destructibleMaster.GetDestructibleSettings();
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
