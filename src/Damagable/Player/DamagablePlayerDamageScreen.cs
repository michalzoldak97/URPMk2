using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace URPMk2
{
	public class DamagablePlayerDamageScreen : MonoBehaviour
	{
		[SerializeField] private float maxAlpha;
		[SerializeField] private float secToRestore;
		[SerializeField] private Image damageScreen;

		private WaitForSeconds waitForRestore;
		private DamagableMaster dmgMaster;
		private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
			waitForRestore = new WaitForSeconds(secToRestore / 20f);
        }
		
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventReceivedDamage += SetDamageScreen;
		}
		
		private void OnDisable()
		{
            dmgMaster.EventReceivedDamage -= SetDamageScreen;
        }

		private void SetImageAlpha(float toSet)
		{
            Color screenColor = damageScreen.color;
            screenColor.a = toSet;
            damageScreen.color = screenColor;
        }

		private IEnumerator RestoreDamageScreen()
		{
			float alphaStep = damageScreen.color.a / 20f;

            for (int i = 0; i < 20; i++)
			{
                yield return waitForRestore;

                SetImageAlpha(damageScreen.color.a - alphaStep);
            }
            SetImageAlpha(0f);
        }
        private void SetDamageScreen(Transform dummy, float dmg)
		{
			float health = dmgMaster.GetHealth();
            float pDmg = dmg > health ? health : dmg;
			float alphaToSet = Mathf.InverseLerp(0, health, pDmg) * maxAlpha;

			SetImageAlpha(alphaToSet);

            StopAllCoroutines();
			StartCoroutine(RestoreDamageScreen());
        }
	}
}
