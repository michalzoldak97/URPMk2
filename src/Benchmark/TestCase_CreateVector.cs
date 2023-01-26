using UnityEngine;

namespace URPMk2
{
    public class TestVectorContainer
    {
        public Vector3 MyVector3 { get; set; }
        public Vector2 MyVector2 { get; set; }
    }
	public class TestCase_CreateVector : MonoBehaviour, ITestable
	{
        private TestVectorContainer testVecContainer;
        private void Start()
        {
            testVecContainer = new TestVectorContainer();
        }
        private void CreateWithNew()
        {
            Vector3 empty = new Vector3(-1f, -1f, -1f);
            testVecContainer.MyVector3 = empty;
        }
        private void CreateWithAllocated()
        {
            Vector3 empty;
            empty.x = -1f; empty.y = -1f; empty.z = -1f;
            testVecContainer.MyVector3 = empty;
        }
        public void RunPrimaryTestCase()
        {
            CreateWithNew();
        }

        public void RunAlternativeTestCase()
        {
            CreateWithAllocated();
        }
    }
}
