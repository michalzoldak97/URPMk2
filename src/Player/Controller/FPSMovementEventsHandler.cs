using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2 
{ 
    public class FPSMovementEventsHandler : MonoBehaviour
    {
        public delegate void MovementEventHandler(int state);
        public event MovementEventHandler EventStep;
        public event MovementEventHandler EventStoppedMoving;

        public void CallEventStep(int toStateIdx)
        {
            EventStep?.Invoke(toStateIdx);
        }
        public void CallEventStoppedMoving(int toStateIdx)
        {
            EventStoppedMoving?.Invoke(toStateIdx);
        }
    }
}
