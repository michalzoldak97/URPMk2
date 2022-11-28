using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class FSMDynamicDefendState : IFSMState
	{
		private Vector3 targetPoint, shiftVec;
		private Transform fTransform;
		private FSMStateManager fManager;
		public FSMDynamicDefendState(FSMStateManager fManager)
        {
			this.fManager = fManager;
			fTransform = fManager.transform;
			targetPoint = Vector3.zero;
			shiftVec = Vector3.zero;
		}
	
		private bool IsDefendTargetLost()
        {
			if (fManager.MyFollowTarget == null)
			{
				targetPoint = Vector3.zero;
				shiftVec = Vector3.zero;
				return true;
			}
			return false;
		}
		private bool PickDynamicDefendTarget()
        {
			List<ITeamMember> possibleDefendTargets = 
				TeamMembersManager.GetTeamMembersInRange(
					fManager.GetFSMSettings().teamsToDefend, 
					fTransform.position, 
					250000);

			if (possibleDefendTargets.Count < 1)
            {
				targetPoint = Vector3.zero;
				return false;
			}

			int defendTargetIdx = Random.Range(0, possibleDefendTargets.Count);
			fManager.MyFollowTarget = possibleDefendTargets[defendTargetIdx].ObjTransform;
			return true;
        }
		private void KeepDefendPosition()
        {
			if (IsDefendTargetLost() &&
				!PickDynamicDefendTarget())
			{
				fManager.SwitchState(false, fManager.patrolState);
				return;
			}

			if (shiftVec == Vector3.zero)
				shiftVec = Random.insideUnitSphere *
					fManager.GetFSMSettings().shiftRange;

			targetPoint = fManager.MyFollowTarget.position + shiftVec;
			fManager.MyNavMeshAgent.SetDestination(targetPoint);
			fManager.MyNavMeshAgent.isStopped = false;
        }
		private void Defend()
		{
			if (fManager.PursueTarget == null ||
				!fManager.MyNPCMaster.NpcLook.IsPursueTargetVisible(fManager.PursueTarget))
			{
				ITeamMember[] enemiesInRange = fManager.MyNPCMaster.NpcLook.GetEnemiesInRange();

				if (!(System.Array.Exists(enemiesInRange, el => el != null)))
					return;

				float minDist = fManager.SightRangePow * 2;
				float distToEnemy;

				int eirLen = enemiesInRange.Length;
				for (int i = 0; i < eirLen; i++)
				{
					if (enemiesInRange[i] == null)
						continue;

					distToEnemy = (enemiesInRange[i].ObjTransform.position - fTransform.position).sqrMagnitude;

					if (distToEnemy < minDist)
					{
						minDist = distToEnemy;
						fManager.PursueTarget = enemiesInRange[i].ObjTransform;
					}
				}
			}
            fManager.AlertAllies();
            fManager.RotateTowardsTarget();
			fManager.LaunchWeaponSystem();
		}
		public void UpdateState()
		{
			KeepDefendPosition();
			Defend();
		}
	}
}
