using UnityEngine;

namespace URPMk2
{
	public class ExplosiveCollisionFuse : MonoBehaviour
	{
		private LayerMask layersToHit;
        private ExplosiveMaster explosiveMaster;
        private void Start()
		{
            explosiveMaster = GetComponent<ExplosiveMaster>();
			layersToHit = explosiveMaster.GetExplosiveSettings().layersTriggeringExplosion;
        }
        private void OnCollisionEnter(Collision col)
        {
            if ((layersToHit.value & (1 << col.gameObject.layer)) == 0)
                return;

            explosiveMaster.CallEventExplode();
        }
    }
}
