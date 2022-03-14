using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace URPMk2
{
    public class TestCase : ITestable
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
        private void PrimaryTestCase()
        {
            Vector3 someVal = InitializeTestVec3();
            someVal = _left * randIntGenerator.Next(200);
            DoSomeJob(someVal);
        }
        private void AlternativeTestCase()
        {
            Vector3 someVal = InitializeTestVec3();
            someVal = _left * UnityEngine.Random.Range(0, 200);
            DoSomeJob(someVal);
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
