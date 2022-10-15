using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace FSM
{
    public abstract class FSMBase<T1, T2> : MonoBehaviour
    where T1 : Enum
    where T2 : FSMTransitionMessage
    {
        [SerializeField] private T1 _initialState = default;
        
        public FSMState<T1, T2> CurState { get; private set; }
        
        private FSMState<T1, T2>[] _states;

        private FSMState<T1, T2>[] _States
        {
            get
            {
                if (_states == null)
                {
                    _states = GetComponentsInChildren<FSMState<T1, T2>>();
                }

                return _states;
            }
        }
        
        public Action<T1> OnEnterState { get; set; }
        
        public Action<T1> OnExitState { get; set; }

        public bool TryChangeState(T1 targetState, T2 transitionMessage = null)
        {
            FSMState<T1, T2> candidateState = _States.SingleOrDefault(state => state.GetStateType().Equals(targetState));

            if (candidateState == null)
            {
                return false;
            }

            if (CurState != null && !GetTransitionMappings()[CurState.GetStateType()].Contains(targetState))
            {
                return false;
            }

            if (CurState != null)
            {
                CurState.ExitState();
                
                OnExitState?.Invoke(CurState.GetStateType());
            }

            CurState = candidateState;
            
            CurState.EnterState(transitionMessage);
            
            OnEnterState?.Invoke(CurState.GetStateType());

            return true;
        }

        private void Awake()
        {
            TryChangeState(_initialState);
        }

        protected abstract IReadOnlyDictionary<T1, HashSet<T1>> GetTransitionMappings();
    }
}