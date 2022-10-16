using System;
using InputSystem;
using UnityEngine;

namespace CharacterScripts
{
    public class PlayerInputController : MonoBehaviour
    {
        public Action OnMovementInputStarted { get; set; }
        public Action OnMovementInputFinished { get; set; }
        public Action<Vector2> OnMovementInputPerformed { get; set; }

        private bool _horizontalMovement;
        private bool _verticalMovement;
        private Vector2 _movementDirection;
        
        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void LateUpdate()
        {
            if (_horizontalMovement || _verticalMovement)
            {
                OnMovementInputPerformed?.Invoke(_movementDirection);
                
                _movementDirection = Vector2.zero;
            }
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(EInputEvent.HorizontalAxis, OnHorizontalInputPerformed);
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(EInputEvent.VerticalAxis, OnVerticalInputPerformed);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(EInputEvent.HorizontalAxis, OnHorizontalInputPerformed);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(EInputEvent.VerticalAxis, OnVerticalInputPerformed);
        }

        #region Movement

        private void OnHorizontalInputPerformed(InputData_Axis inputData)
        {
            _horizontalMovement = inputData.InputState != EAxisInputState.Finished;
            
            TryTriggerMovementAction(inputData, true);
        }

        private void OnVerticalInputPerformed(InputData_Axis inputData)
        {
            _verticalMovement = inputData.InputState != EAxisInputState.Finished;
            
            TryTriggerMovementAction(inputData, false);
        }

        private void TryTriggerMovementAction(InputData_Axis inputData, bool horizontalAxis)
        {
            if (inputData.InputState == EAxisInputState.Started)
            {
                OnMovementInputStarted?.Invoke();

                return;
            }

            if (inputData.InputState == EAxisInputState.Finished)
            {
                if (_verticalMovement && _horizontalMovement)
                {
                    OnMovementInputFinished?.Invoke();
                }
                
                return;
            }

            Vector2 input = horizontalAxis ? new Vector2(inputData.Amount, 0) : new Vector2(0, inputData.Amount);

            _movementDirection += input;
        }

        #endregion
    }
}