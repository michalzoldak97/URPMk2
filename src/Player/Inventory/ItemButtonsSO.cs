using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemButtons", order = 100)]
	public class ItemButtonsSO : ScriptableObject
	{
		public GameObject[] itemButtons;
	}
}
