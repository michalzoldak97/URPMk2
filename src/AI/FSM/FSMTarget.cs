using UnityEngine;

namespace URPMk2
{
	public class FSMTarget
	{
		public bool isVisible;
		public Transform targetTransform;

		public FSMTarget(bool isVisible, Transform targetTransform)
        {
			this.isVisible = isVisible;
			this.targetTransform = targetTransform;
		}
	}
}
