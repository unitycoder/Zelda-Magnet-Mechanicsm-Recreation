using System;
using UnityEngine;

namespace Core.InputSystem
{
    public class InputListener_KeyHold : InputListenerBase<InputData_Key>
    {
        [SerializeField] private KeyCode[] _targetKeyCodes = null;
        
        public override EInputEvent GetInputEventType()
        {
            return EInputEvent.KeyHold;
        }

        public override Action<InputData_Key> OnInputEventTriggered { get; set; }

        private void Update()
        {
            foreach (KeyCode targetKeyCode in _targetKeyCodes)
            {
                if (Input.GetKey(targetKeyCode))
                {
                    OnInputEventTriggered?.Invoke(
                        new InputData_Key(
                            GetInputEventType(),
                            targetKeyCode));
                }
            }
        }
    }
}