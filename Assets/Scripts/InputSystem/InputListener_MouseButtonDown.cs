using System;
using UnityEngine;

namespace InputSystem
{
    public class InputListener_MouseButtonDown : InputListenerBase<InputData_Mouse>
    {
        [SerializeField] private int[] _targetMouseButtons = null;

        #region Events

        public override Action<InputData_Mouse> OnInputEventTriggered { get; set; }

        #endregion
        
        public override EInputEvent GetInputEventType()
        {
            return EInputEvent.MouseButtonDown;
        }

        private void Update()
        {
            foreach (int mouseButton in _targetMouseButtons)
            {
                if (Input.GetMouseButtonDown(mouseButton))
                {
                    OnInputEventTriggered?.Invoke(
                        new InputData_Mouse(
                            GetInputEventType(),
                            mouseButton,
                            Input.mousePosition));
                }
            }
        }
    }
}