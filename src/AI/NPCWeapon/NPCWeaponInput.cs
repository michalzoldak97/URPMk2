using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponInput : MonoBehaviour
	{
		private Transform myTransform;
		private NPCMaster npcMaster;
		private NPCWeaponShootFieldValidator shootFieldValidator;
		private void SetInit()
		{
			npcMaster = GetComponent<NPCMaster>();
			shootFieldValidator = GetComponent<NPCWeaponShootFieldValidator>();
		}
		
		private void OnEnable()
		{
			SetInit();
			npcMaster.EventAttackTarget += OnAttackTarget;
		}
		
		private void OnDisable()
		{
			npcMaster.EventAttackTarget -= OnAttackTarget;
		}
		private float CalculateDotProd(Transform target)
		{
			return Vector3.Dot((target.position - myTransform.position).normalized, myTransform.forward);
		}
		protected void AttemptToShoot(Transform target, INPCWeaponController weaponController)
        {
			if (CalculateDotProd(target) > .99f &&
				shootFieldValidator.IsShootFieldClean(transform))
				weaponController.LaunchAtack();
        }
		protected virtual void OnAttackTarget(Transform target)
        {

		}
		protected virtual void Start()
        {
			myTransform = transform;
		}
	}
}
