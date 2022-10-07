using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class NPCLook : MonoBehaviour
	{
		[SerializeField] private Transform head;
		private int sightRangePow;
		private Vector3 heading;
		private Transform myTransform;
		private FSMTarget targetNotFound;
		private NPCMaster npcMaster;
		private FSMSettingsSO fsmSettings;
		private VisibilityParamContainer visibilityParams;
		private ITeamMember[] enemiesBuffer;
		private void SetInit()
		{
			myTransform = transform;
			targetNotFound = new FSMTarget(false, null);
			npcMaster = GetComponent<NPCMaster>();
			fsmSettings = npcMaster.GetFSMSettings();
			sightRangePow = fsmSettings.sightRange * fsmSettings.sightRange;
			enemiesBuffer = new ITeamMember[fsmSettings.enemiesBufferSize];
			visibilityParams = new VisibilityParamContainer(
				fsmSettings.sightRange,
				fsmSettings.highResDetectionRange,
				fsmSettings.highResDetectionRange * fsmSettings.highResDetectionRange,
				fsmSettings.sightLayers,
				head);
		}
		
		private void Awake()
		{
			SetInit();
		}
		private float CalculateDotProd(Transform target)
		{
			heading = (target.position - myTransform.position).normalized;
			return Vector3.Dot(heading, myTransform.forward);
		}
		public FSMTarget IsTargetVisible()
		{
			List<ITeamMember> enemiesInRange = TeamMembersManager.GetTeamMembersInRange(
					fsmSettings.teamsToAttack,
					myTransform.position,
					sightRangePow
				);

			int numEnemies = enemiesInRange.Count;
			for (int i = 0; i < numEnemies; i++)
			{
				bool isInHighResRange = heading.sqrMagnitude < visibilityParams.highResSearchSqrRange;
				if (CalculateDotProd(enemiesInRange[i].ObjTransform) < fsmSettings.minDotProd &&
					!isInHighResRange)
					continue;

				if (VisibilityCalculator.IsVisibleSingle(visibilityParams, heading, enemiesInRange[i].ObjTransform)
					|| (isInHighResRange &&
					VisibilityCalculator.IsVisibleCorners(visibilityParams, enemiesInRange[i].ObjTransform, enemiesInRange[i].BoundsExtens)))
					return new FSMTarget(true, enemiesInRange[i].ObjTransform);
			}
			return targetNotFound;
		}

		public ITeamMember[] GetEnemiesInRange()
		{
			System.Array.Clear(enemiesBuffer, 0, enemiesBuffer.Length);

			List<ITeamMember> enemiesInRange = TeamMembersManager.GetTeamMembersInRange(
					fsmSettings.teamsToAttack,
					myTransform.position,
					sightRangePow
				);

			Teams topTeam = fsmSettings.teamID;

			int enemiesAdded = 0;
			int numEnemies = enemiesInRange.Count;
			for (int i = 0; i < numEnemies; i++)
			{
				if (enemiesAdded >= enemiesBuffer.Length)
					break;

				bool isInHighResRange = heading.sqrMagnitude < visibilityParams.highResSearchSqrRange;

				if (CalculateDotProd(enemiesInRange[i].ObjTransform) < fsmSettings.minDotProd &&
					!isInHighResRange)
					continue;

				if (VisibilityCalculator.IsVisibleSingle(visibilityParams, heading, enemiesInRange[i].ObjTransform)
					|| (isInHighResRange &&
					VisibilityCalculator.IsVisibleCorners(visibilityParams, enemiesInRange[i].ObjTransform, enemiesInRange[i].BoundsExtens)))
				{
					if (topTeam == fsmSettings.teamID)
						topTeam = enemiesInRange[i].TeamID;
					else if (enemiesInRange[i].TeamID != topTeam)
						break;

					enemiesBuffer[enemiesAdded] = enemiesInRange[i];
					enemiesAdded++;
				}
			}
			return enemiesBuffer;
		}
	}
}
