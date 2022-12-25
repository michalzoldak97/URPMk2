namespace URPMk2
{
	public class NPCTurrentRotationController : NPCRotationController
    {
        protected override void SetParams()
        {
            FSMSettingsSO settings = GetComponent<NPCMaster>().GetFSMSettings();
            rotationAngularSpeed = settings.rotationAngularSpeed;
            rotationsPerCycle = settings.rotationsPerCycle;
            float checkRate = GetComponent<NPCTurrent>().GetCheckRate();
            nextRot = (checkRate - 0.1f) / rotationsPerCycle;
        }
    }
}
