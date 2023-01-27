using System;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.Serialization;

namespace URPMk2
{
	public class MyDecisionRequester : MonoBehaviour
	{
            [Range(1, 200)]
            public int DecisionPeriod = 5;

            [FormerlySerializedAs("RepeatAction")]
            public bool TakeActionsBetweenDecisions = true;

            [NonSerialized]
            Agent m_Agent;

            public Agent Agent
            {
                get => m_Agent;
            }

            internal void Awake()
            {
                m_Agent = gameObject.GetComponent<Agent>();
                Debug.Assert(m_Agent != null, "Agent component was not found on this gameObject and is required.");
                Academy.Instance.AgentPreStep += MakeRequests;
            }

            void OnDestroy()
            {
                if (Academy.IsInitialized)
                {
                    Academy.Instance.AgentPreStep -= MakeRequests;
                }
            }

            public struct DecisionRequestContext
            {
                public int AcademyStepCount;
            }

            /// <param name="academyStepCount">The current step count of the academy.</param>
            void MakeRequests(int academyStepCount)
            {
                var context = new DecisionRequestContext
                {
                    AcademyStepCount = academyStepCount
                };

                if (ShouldRequestDecision(context))
                {
                    m_Agent?.RequestDecision();
                }

                if (ShouldRequestAction(context))
                {
                    m_Agent?.RequestAction();
                }
            }
            /// <param name="context"></param>
            /// <returns></returns>
            protected virtual bool ShouldRequestDecision(DecisionRequestContext context)
            {
                return context.AcademyStepCount % DecisionPeriod == 0;
            }

            /// <param name="context"></param>
            /// <returns></returns>
            protected virtual bool ShouldRequestAction(DecisionRequestContext context)
            {
                return TakeActionsBetweenDecisions;
            }
    }
}
