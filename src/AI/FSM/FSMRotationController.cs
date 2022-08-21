namespace URPMk2
{
	public class FSMRotationController : NPCRotationController
	{
		protected override void SetParams()
		{
			FSMStateManager fManager = GetComponent<FSMStateManager>();
			rotationAngularSpeed = fManager.GetFSMSettings().rotationAngularSpeed;
			rotationsPerCycle = fManager.GetFSMSettings().rotationsPerCycle;
			nextRot = (fManager.GetCheckRate() - 0.1f) / rotationsPerCycle;
		}
	}
}
