using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class TestCase : ITestable
    {
        private Vector3 _left = Vector3.left;
        private float testX = 23.5f;
        private float testY = 23.5f;
        private float[] testXY = new float[] { 23.5f, 23.5f };
        private Vector3 InitializeTestVec3()
        {
            Vector3 someVals = Vector3.zero;
            someVals.x = -2.5f;
            someVals.y = 2.5f;
            someVals.z = 4.7f;
            return someVals;
        }
        private void DoSomeJob()
        {
            Vector3 doSomeJob = new Vector3();
            doSomeJob.x = Mathf.Sqrt(123.3f - 431.5f);
            doSomeJob.y = Mathf.Sqrt(324.6f * 7.1f);
            doSomeJob.z = Mathf.Sqrt(435.1f / 23.5f);
        }
        private void PrimaryTestCase()
        {
            Vector3 someVal = InitializeTestVec3();
            someVal = _left * (Mathf.Sqrt(testXY[0]) + Mathf.Sqrt(testXY[1]));
            DoSomeJob();
        }
        private void AlternativeTestCase()
        {
            Vector3 someVal = InitializeTestVec3();
            someVal = _left * (Mathf.Sqrt(testX) + Mathf.Sqrt(testY));
            DoSomeJob();
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
