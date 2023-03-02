using UnityEngine;
using UnityEngine.SceneManagement;

namespace URPMk2
{
	public class MainSceneManager : MonoBehaviour
	{
		public void LoadSelectedScene(int idx)
		{
			SceneManager.LoadScene(idx);
		}
	}
}
