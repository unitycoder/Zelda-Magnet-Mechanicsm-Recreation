using System;
using UnityEngine;

namespace InputSystem
{
    public class InputListener_MouseButtonUp : InputListenerBase<InputData_Mouse>
    {
        [SerializeField] private int[] _targetMouseButtons = null;
        
        #region Events

        public override Action<InputData_Mouse> OnInputEventTriggered { get; set; }

        #endregion
        
        public override EInputEvent GetInputEventType()
        {
            return EInputEvent.MouseButtonUp;
        }
        
        private void Update()
        {
            foreach (int targetMouseButton in _targetMouseButtons)
            {
                if (Input.GetMouseButtonUp(targetMouseButton))
                {
                    OnInputEventTriggered?.Invoke(
                        new InputData_Mouse(
                            GetInputEventType(),
                            targetMouseButton,
                            Input.mousePosition));
                }
            }
        }
    }
}