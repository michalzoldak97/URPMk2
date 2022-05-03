using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class TestCase : MonoBehaviour, ITestable
    {
        private Vector3 _left = Vector3.left;
        private System.Random randIntGenerator = new System.Random();
        private Vector3 InitializeTestVec3()
        {
            Vector3 someVals = Vector3.zero;
            someVals.x = -2.5f;
            someVals.y = 2.5f;
            someVals.z = 4.7f;
            return someVals;
        }
        private void DoSomeJob(Vector3 doSomeJob)
        {
            doSomeJob.x = Mathf.Sqrt(123.3f - 431.5f);
            doSomeJob.y = Mathf.Sqrt(324.6f * 7.1f);
            doSomeJob.z = Mathf.Sqrt(435.1f / 23.5f);
        }

        private IEnumerator UseNewCoroutine()
        {
            yield return new WaitForSeconds(1);
            DoSomeJob(InitializeTestVec3());
        }
        private IEnumerator UseCachedCoroutine()
        {
            yield return GameConfig.waitEffectAlive;
            DoSomeJob(InitializeTestVec3());
        }
        private void PrimaryTestCase()
        {
            StartCoroutine(UseCachedCoroutine());
        }
        private void AlternativeTestCase()
        {
            StartCoroutine(UseNewCoroutine());
        }
        public void RunPrimaryTestCase()
        {
            PrimaryTestCase();
        }
        public void RunAlternativeTestCase()
        {
            AlternativeTestCase();
        }
    }
}
