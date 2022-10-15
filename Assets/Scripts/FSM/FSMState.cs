using System;
using UnityEngine;

namespace FSM
{
    public abstract class FSMState<T1, T2> : MonoBehaviour
    where T1 : Enum
    where T2 : FSMTransitionMessage
    {
        public abstract T1 GetStateType();

        public void EnterState(T2 transitionMessage = null)
        {
            EnterStateCustomActions(transitionMessage);
        }

        public void ExitState()
        {
            ExitStateCustomActions();
        }
        
        protected virtual void EnterStateCustomActions(T2 transitionMessage = null) { }
        
        protected virtual void ExitStateCustomActions() { }
    }
}