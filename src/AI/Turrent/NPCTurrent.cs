using UnityEngine;

namespace URPMk2
{
	public class NPCTurrent : MonoBehaviour
	{
		private bool isEnemyLocked;
		private int enemyDetectionCount, enemiesBuffLen;
		private float checkRate, nextCheck;
		private Transform target;
		private NPCMaster npcMaster;
        private FSMRotationController rotationController;
        private void Start()
		{
			rotationController = GetComponent<FSMRotationController>();
            npcMaster = GetComponent<NPCMaster>();
			checkRate = npcMaster.GetFSMSettings().checkRate;
            checkRate = Random.Range(
                checkRate - npcMaster.GetFSMSettings().checkRateOffset,
                checkRate + npcMaster.GetFSMSettings().checkRateOffset);
			enemiesBuffLen = npcMaster.GetFSMSettings().enemiesBufferSize;
        }
		private void ScanForEnemies()
		{
            if (target != null &&
				npcMaster.NpcLook.IsPursueTargetVisible(target))
			{
				enemyDetectionCount++;
				if (enemyDetectionCount >= npcMaster.GetFSMSettings().requiredDetectionCount)
					isEnemyLocked = true;
				return;
			}

			enemyDetectionCount = 0;
			int emptyDetections = 0;

            ITeamMember[] enemiesInRange = npcMaster.NpcLook.GetEnemiesInRange();
			for (int i = 0; i < enemiesBuffLen; i++)
			{
				if (enemiesInRange[i].ObjTransform != null)
				{
					target = enemiesInRange[i].ObjTransform;
					return;
                }

				emptyDetections++;
            }

			if (emptyDetections >= enemiesBuffLen)
				target = null;
        }
		
		private void AttackTarget()
		{
			npcMaster.CallEventAttackTarget(target);
		}
		private void Update()
		{
			if (Time.time < nextCheck)
				return;

			nextCheck = Time.time + checkRate;

			ScanForEnemies();

			if (isEnemyLocked)
                AttackTarget(); 
		}
	}
}
