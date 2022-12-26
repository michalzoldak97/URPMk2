using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class NPCTurrent : MonoBehaviour
	{
		private bool isEnemyLocked, isInformingAllies;
		private int enemyDetectionCount, enemiesBuffLen;
		private float checkRate, nextCheck;
		private Transform target;
		private WaitForSeconds waitForNextAlliesInform;
        private NPCMaster npcMaster;
        private NPCTurrentRotationController rotationController;

		public float GetCheckRate()
		{
			return checkRate;
		}
        private void Awake()
		{
            npcMaster = GetComponent<NPCMaster>();
			checkRate = npcMaster.GetFSMSettings().checkRate;
            checkRate = Random.Range(
                checkRate - npcMaster.GetFSMSettings().checkRateOffset,
                checkRate + npcMaster.GetFSMSettings().checkRateOffset);
        }
		private void Start()
		{
            enemiesBuffLen = npcMaster.GetFSMSettings().enemiesBufferSize;
            waitForNextAlliesInform = new WaitForSeconds(npcMaster.GetFSMSettings().informAlliesPeriod);
            rotationController = GetComponent<NPCTurrentRotationController>();
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
				if (enemiesInRange[i] != null)
				{
					target = enemiesInRange[i].ObjTransform;
					return;
                }

				emptyDetections++;
            }

			if (emptyDetections >= enemiesBuffLen)
				target = null;
        }
        private IEnumerator ResetInformState()
        {
            yield return waitForNextAlliesInform;
            isInformingAllies = false;
        }
        private void AlertAllies()
        {
            if (!npcMaster.GetFSMSettings().shouldInformAllies ||
                isInformingAllies ||
                !gameObject.activeSelf)
                return;

            isInformingAllies = true;

            List<ITeamMember> teamMembersInRange = TeamMembersManager.GetTeamMembersInRange(
				npcMaster.GetFSMSettings().teamID, 
				transform.position, 
				npcMaster.GetFSMSettings().informAlliesRangePow);

            int numAllies = teamMembersInRange.Count;

            for (int i = 0; i < numAllies; i++)
            {
                if (teamMembersInRange[i].Object.activeSelf &&
                    teamMembersInRange[i].Object != gameObject)
                    teamMembersInRange[i].NMaster.CallEventAlertAboutEnemy(target);
            }

            StartCoroutine(ResetInformState());
        }
        private void AttackTarget()
		{
			rotationController.StartRotateTowardsTransform(target);
            npcMaster.CallEventAttackTarget(target);
			AlertAllies();
        }
		private void Update()
		{
			if (Time.time < nextCheck)
				return;

			nextCheck = Time.time + checkRate;

			ScanForEnemies();

			if (isEnemyLocked &&
				target != null)
                AttackTarget(); 
		}
	}
}
