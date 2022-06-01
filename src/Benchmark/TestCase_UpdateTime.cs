using UnityEngine;

namespace URPMk2
{
	public class TestCase_UpdateTime : MonoBehaviour, ITestable
	{
        private float toAdd;
		public void RunPrimaryTestCase()
        {
			for (int i = 0; i < 10000; i++)
            {
                if (Time.time < toAdd)
                    toAdd -= 0.000001f;

                if (i % 100 == 0)
                {
                    toAdd += Time.time / 1000;
                }
            }
        }
		public void RunAlternativeTestCase()
        {
            for (int i = 0; i < 10000; i++)
            {
                float t = Time.time;
                if (t < toAdd)
                    toAdd -= 0.000001f;

                if (i % 100 == 0)
                {
                    toAdd += t / 1000;
                }
            }
        }
	}
}
