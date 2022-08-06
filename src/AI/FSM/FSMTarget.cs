using UnityEngine;

namespace URPMk2
{
	public struct FSMTarget
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
