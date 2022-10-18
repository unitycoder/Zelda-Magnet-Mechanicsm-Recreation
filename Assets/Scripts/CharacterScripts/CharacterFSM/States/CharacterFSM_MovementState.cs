using Core.FSM;
using UnityEngine;
using Core.Common;
using Core.Common.Animation;
using Util;

namespace CharacterSystem
{
    public class CharacterFSM_MovementState : FSMState<ECharacterState, CharacterFSMTransitionMessage>
    {
        [SerializeField] private PlayerInputController _inputController = null;
        
        [SerializeField] private MoveBehaviour_CharacterController _moveBehaviour = null;

        [SerializeField] private RotationBehaviour _rotationBehaviour = null;

        [SerializeField] private AnimationController _animationController = null;

        public override ECharacterState GetStateType()
        {
            return ECharacterState.Move;
        }

        protected override void EnterStateCustomActions(CharacterFSMTransitionMessage transitionMessage = null)
        {
            _inputController.OnMovementInputPerformed += OnInputPerformed;
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _inputController.OnMovementInputPerformed -= OnInputPerformed;
            
            _animationController.SetParameter(Constants.MovementSpeedParamName, 0);
            
            base.ExitStateCustomActions();
        }

        private void Awake()
        {
            RegisterToInputController();
        }

        private void OnDestroy()
        {
            UnregisterFromInputController();
        }

        private void RegisterToInputController()
        {
            _inputController.OnMovementInputStarted += OnInputStarted;
            _inputController.OnMovementInputFinished += OnInputCompleted;
        }

        private void UnregisterFromInputController()
        {
            _inputController.OnMovementInputStarted -= OnInputStarted;
            _inputController.OnMovementInputFinished -= OnInputCompleted;
        }

        private void OnInputStarted()
        {
            FSM.TryChangeState(ECharacterState.Move);
        }

        private void OnInputCompleted()
        {
            FSM.TryChangeState(ECharacterState.Idle);
        }

        private void OnInputPerformed(Vector2 direction)
        {
            Vector2 normalizedInput = direction.magnitude > 1 ? direction.normalized : direction;

            Vector3 worldDirection = Camera.main.transform.TransformDirection(new Vector3(normalizedInput.x, 0, normalizedInput.y));

            worldDirection.y = 0;
            
            _moveBehaviour.Move(worldDirection);
            
            _rotationBehaviour.Rotate(worldDirection);
            
            _animationController.SetParameter(Constants.MovementSpeedParamName, normalizedInput.magnitude);
        } 
    }
}