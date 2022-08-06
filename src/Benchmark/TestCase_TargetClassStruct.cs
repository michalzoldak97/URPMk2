using UnityEngine;

namespace URPMk2
{
    public class TestClassDataContainer
    {
        public bool b;
        public float f;
        public Vector3 v;
        public TestClassDataContainer(bool b, float f, Vector3 v)
        {
            this.b = b;
            this.f = f;
            this.v = v;
        }
    }
    public struct TestStructDataContainer
    {
        public bool b;
        public float f;
        public Vector3 v;
        public TestStructDataContainer(bool b, float f, Vector3 v)
        {
            this.b = b;
            this.f = f;
            this.v = v;
        }
    }
    public class TestStructClassExecutor
    {
        public void DoSthWithClass(TestClassDataContainer c)
        {
            if (c.b)
            {
                c.f += 0.0034f;
                c.v.x += 0.54f;
            }
        }
        public void DoSthWithStruct(TestStructDataContainer s)
        {
            if (s.b)
            {
                s.f += 0.0034f;
                s.v.x += 0.54f;
            }
        }
    }
    public class TestCase_TargetClassStruct : MonoBehaviour, ITestable
	{
        TestClassDataContainer c;
        TestStructDataContainer s;
        TestStructClassExecutor t;
        private void InitializeData()
        {
            t = new TestStructClassExecutor();
            c = new TestClassDataContainer(true, 453.55f, new Vector3(23.5f, 543.6f - 34f));
            s = new TestStructDataContainer(true, 453.55f, new Vector3(23.5f, 543.6f - 34f));
        }
        public void RunPrimaryTestCase()
        {
            t.DoSthWithClass(c);
            if (c.b)
            {
                c.f += 0.0034f;
                c.v.x += 0.54f;
            }
            t.DoSthWithClass(c);
        }

        public void RunAlternativeTestCase()
        {
            t.DoSthWithStruct(s);
            if (s.b)
            {
                s.f += 0.0034f;
                s.v.x += 0.54f;
            }
            t.DoSthWithStruct(s);
        }

        private void Start()
        {
            InitializeData();
        }
    }
}
