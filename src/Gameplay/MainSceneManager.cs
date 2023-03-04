using UnityEngine;
using UnityEngine.SceneManagement;

namespace URPMk2
{
	public class MainSceneManager : MonoBehaviour
	{
		private bool freeFPS;
		public void LoadSelectedScene(int idx)
		{
			SceneManager.LoadScene(idx);
		}
		public void ToggleFrameRate()
		{
			freeFPS = !freeFPS;

			if (freeFPS)
			{
				Application.targetFrameRate = 999;
				QualitySettings.vSyncCount = 0;
			} 
			else
			{
                Application.targetFrameRate = 60;
                QualitySettings.vSyncCount = 1;
            }
		}

        private void Start()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 1;
        }
    }
}
