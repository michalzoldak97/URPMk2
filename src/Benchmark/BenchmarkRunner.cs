using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class BenchmarkRunner : MonoBehaviour
    {
        [SerializeField] public int numOfCases = 10000;
        [SerializeField] public float waitUntilStart = 1f;
        private float _numTests = 40;
        private Vector2 primaryRes = Vector2.zero;
        private Vector2 alternativeRes = Vector2.zero;
        private ITestable _testCase = new TestCase();

        private IEnumerator RunPrimary()
        {
            yield return new WaitForSeconds(0.1f);
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            for (int a = 0; a < numOfCases; a++)
            {
                _testCase.RunPrimaryTestCase();
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
                _testCase.RunAlternativeTestCase();
            }
            watch.Stop();
            alternativeRes.x += watch.ElapsedMilliseconds;
            alternativeRes.y += watch.ElapsedTicks;
        }
        private IEnumerator RunBenchmark()
        {
            for (int i = 0; i < _numTests; i++)
            {
                StartCoroutine(RunPrimary());
                StartCoroutine(RunAlternative());
            }
            yield return new WaitForSeconds(10f);
            UnityEngine.Debug.Log("\nPrimary milisec = " + (primaryRes.x / _numTests).ToString() + "    Primary tics = " + (primaryRes.y / _numTests).ToString()
                + "\nAlternative milisec = " + (alternativeRes.x / _numTests).ToString() + "    Alternative tics = " + (alternativeRes.y / _numTests).ToString());
        }
        private void Start()
        {
            StartCoroutine(RunBenchmark());
        }
    }
}