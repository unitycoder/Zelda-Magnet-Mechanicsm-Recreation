using System;
using UnityEngine;

namespace InputSystem
{
    public class InputListener_HorizontalAxis : InputListenerBase<InputData_Axis>
    {
        #region Constants

        private const string AXIS_NAME = "Horizontal";

        #endregion

        [SerializeField] private bool _getAxisRaw = false;
   
        private EAxisInputState _inputState = EAxisInputState.None;

        #region Events

        public override Action<InputData_Axis> OnInputEventTriggered { get; set; }

        #endregion
        
        public override EInputEvent GetInputEventType()
        {
            return EInputEvent.HorizontalAxis;
        }

        private void Update()
        {
            float inputAmount = _getAxisRaw ? Input.GetAxisRaw(AXIS_NAME) : Input.GetAxis(AXIS_NAME);
            
            if (inputAmount == 0 && _inputState == EAxisInputState.Continuing)
            {
                _inputState = EAxisInputState.Finished;
                
                OnInputEventTriggered?.Invoke(new InputData_Axis(
                    EInputEvent.HorizontalAxis, inputAmount, _inputState));
            }

            if (inputAmount == 0)
            {
                return;
            }
            
            if (_inputState.Equals(EAxisInputState.Started))
            {
                _inputState = EAxisInputState.Continuing;
            }
                
            if (_inputState.Equals(EAxisInputState.None) 
                || _inputState.Equals(EAxisInputState.Finished))
            {
                _inputState = EAxisInputState.Started;
            }
                
            OnInputEventTriggered?.Invoke(new InputData_Axis(
                EInputEvent.HorizontalAxis, inputAmount, _inputState));
        }
    }
}