using System.Threading.Tasks;
using UnityEngine;

namespace URPMk2
{
    public class PoolInstance
    {
        public string poolTag;
        public PooledObjectInstance[] objects;
        private bool isLocked;
        private int objIdx;

        public bool HasFreeObjects()
        {
            return !isLocked;
        }
        private async void SlowDown()
        {
            isLocked = true;
            await Task.Delay(100);
            objIdx = 0;
            isLocked = false;
        }
        public PooledObjectInstance GetObject()
        {
            objIdx++;

            if (objIdx >= objects.Length)
                SlowDown();

            return objects[objIdx - 1];
        }
        public void SupplementObj(PooledObjectInstance obj)
        {
            objects[objIdx] = obj;
        }
    }
}
