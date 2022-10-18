using Core.Common.Animation;
using Core.FSM;
using UnityEngine;
using Util;

namespace CharacterSystem
{
    public class CharacterFSM_IdleState : FSMState<ECharacterState, CharacterFSMTransitionMessage>
    {
        [SerializeField] private AnimationController _animationController = null;
        
        public override ECharacterState GetStateType()
        {
            return ECharacterState.Idle;
        }

        protected override void EnterStateCustomActions(CharacterFSMTransitionMessage transitionMessage = null)
        {
            _animationController.CrossFadeIntoState(Constants.MovementStateName);
            
            _animationController.SetParameter(Constants.MovementSpeedParamName, 0);
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}