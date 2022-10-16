using UnityEngine;

namespace InputSystem
{
    public class InputData_Mouse : InputData
    {
        public int MouseButton { get; }
    
        public Vector3 CursorPosition { get; }

        public InputData_Mouse(EInputEvent inputEventType, int mouseButton, Vector3 cursorPosition) 
            : base(inputEventType)
        {
            MouseButton = mouseButton;
            CursorPosition = cursorPosition;
        }   
    }
}