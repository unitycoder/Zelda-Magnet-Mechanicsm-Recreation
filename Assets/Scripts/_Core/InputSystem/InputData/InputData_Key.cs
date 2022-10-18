using UnityEngine;

namespace Core.InputSystem
{
    public class InputData_Key : InputData
    {
        public KeyCode KeyCode { get; } = KeyCode.None;

        public InputData_Key(EInputEvent inputEventType, KeyCode keyCode) 
            : base(inputEventType)
        {
            KeyCode = keyCode;
        }
    }
}