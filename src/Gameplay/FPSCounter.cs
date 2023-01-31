using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace URPMk2
{
	public class FPSCounter : MonoBehaviour
	{
        private int deltasIndex;
        private readonly float[] timeDeltas = new float[600];
        private readonly List<int> measurements = new List<int>();

        private void AddMeasurement()
        {
            int deltasLen = timeDeltas.Length;
            float total = 0f;

            for (int i = 0; i < deltasLen; i++)
            {
                total += timeDeltas[i];
            }

            measurements.Add((int)(deltasLen / total));
        }
        private void CountFPS()
        {
            if (deltasIndex >= timeDeltas.Length - 1)
            {
                AddMeasurement();
                deltasIndex = 0;
            }
            timeDeltas[deltasIndex] = Time.unscaledDeltaTime;
            deltasIndex++;
        }
        private void Update()
        {
            CountFPS();
        }


        public string GetStats()
        {
            string res = string.Format("AVG: {0}, MAX: {1}, MIN: {2}", 
                measurements.Average(),
                measurements.Max(),
                measurements.Min());

            return res;
        }
    }
}
