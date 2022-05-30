using UnityEngine;

namespace URPMk2
{
	public interface IFSMState
	{
		public void UpdateState();
		public void ToPatrolState();
		public void ToAlertState();
		public void ToPursueState();
		public void ToAttackState();
	}
}
