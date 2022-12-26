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
        protected virtual void Start()
        {
            myTransform = transform;
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
        protected float CalculateDotProd(Transform target)
		{
			return Vector3.Dot((target.position - myTransform.position).normalized, myTransform.forward);
		}
		protected virtual void AttemptToShoot(Transform target, INPCWeaponController weaponController)
        {
            if (CalculateDotProd(target) > .99f &&
				shootFieldValidator.IsShootFieldClean(transform))
				weaponController.LaunchAtack(myTransform);
        }
		protected virtual void OnAttackTarget(Transform target)
        {

		}
	}
}
