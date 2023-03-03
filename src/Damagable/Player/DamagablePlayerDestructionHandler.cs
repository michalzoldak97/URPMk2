using UnityEngine;

namespace URPMk2
{
	public class DamagablePlayerDestructionHandler : MonoBehaviour
	{
		[SerializeField] private Transform[] spawnPositions;
		private DamagableMaster dmgMaster;
		private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventDestroyObject += RelocatePlayer;
		}
		
		private void OnDisable()
		{
            dmgMaster.EventDestroyObject -= RelocatePlayer;
        }
		private void RelocatePlayer(Transform dummy)
		{
			int idx = Random.Range(0, spawnPositions.Length - 1);

			GetComponent<CharacterController>().enabled = false;

			transform.SetPositionAndRotation(spawnPositions[idx].position, spawnPositions[idx].rotation);

            GetComponent<CharacterController>().enabled = true;
        }
	}
}
