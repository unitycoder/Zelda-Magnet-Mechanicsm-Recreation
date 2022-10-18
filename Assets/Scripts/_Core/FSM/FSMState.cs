using System;
using UnityEngine;

namespace Core.FSM
{
    public abstract class FSMState<T1, T2> : MonoBehaviour
    where T1 : Enum
    where T2 : FSMTransitionMessage
    {
        private FSMBase<T1, T2> _fsm;

        public FSMBase<T1, T2> FSM
        {
            get
            {
                if (_fsm == null)
                {
                    _fsm = GetComponentInParent<FSMBase<T1, T2>>();
                }

                return _fsm;
            }
        }
        
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