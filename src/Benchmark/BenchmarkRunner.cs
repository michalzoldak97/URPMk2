using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace URPMk2
{
    public class BenchmarkRunner : MonoBehaviour
    {
        [SerializeField] public int numOfCases = 10000;
        [SerializeField] public float waitUntilStart = 1f;
        private float numTests = 40;
        private Vector2 primaryRes = Vector2.zero;
        private Vector2 alternativeRes = Vector2.zero;
        private ITestable testCase;

        private IEnumerator RunPrimary()
        {
            yield return new WaitForSeconds(0.1f);
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            for (int a = 0; a < numOfCases; a++)
            {
                testCase.RunPrimaryTestCase();
            }
            watch.Stop();
            primaryRes.x += watch.ElapsedMilliseconds;
            primaryRes.y += watch.ElapsedTicks;
        }
        private IEnumerator RunAlternative()
        {
            yield return new WaitForSeconds(0.1f);
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            for (int a = 0; a < numOfCases; a++)
            {
                testCase.RunAlternativeTestCase();
            }
            watch.Stop();
            alternativeRes.x += watch.ElapsedMilliseconds;
            alternativeRes.y += watch.ElapsedTicks;
        }
        private IEnumerator RunBenchmark()
        {
            for (int i = 0; i < numTests; i++)
            {
                StartCoroutine(RunPrimary());
                StartCoroutine(RunAlternative());
            }
            yield return new WaitForSeconds(10f);
            UnityEngine.Debug.Log("\nPrimary milisec = " + (primaryRes.x / numTests).ToString() + "    Primary tics = " + (primaryRes.y / numTests).ToString()
                + "\nAlternative milisec = " + (alternativeRes.x / numTests).ToString() + "    Alternative tics = " + (alternativeRes.y / numTests).ToString());
        }
        private void Start()
        {
            testCase = gameObject.AddComponent<TestCase>();
            StartCoroutine(RunBenchmark());
        }
    }
}