using UnityEngine;

namespace URPMk2
{
	public class MLSpawnReplaceOnDamage : MonoBehaviour
	{
		[SerializeField] GameObject toSpawn;
		private Vector3 startPos;
        private DamagableMaster dmgMaster;
        private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
			startPos = transform.position;
        }
		
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventHealthLow += SpawnReplaceAgent;
		}
		
		private void OnDisable()
		{
            dmgMaster.EventHealthLow -= SpawnReplaceAgent;
        }
		private void SpawnReplaceAgent()
		{
			Instantiate(toSpawn, startPos, transform.rotation);
		}
	}
}
