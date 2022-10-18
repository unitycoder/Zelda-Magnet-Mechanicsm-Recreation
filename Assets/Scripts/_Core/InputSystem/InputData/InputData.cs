using Core.InputSystem;

namespace Core.InputSystem
{
    public abstract class InputData
    {
        public EInputEvent InputEventType { get; }

        protected InputData(EInputEvent inputEventType)
        {
            InputEventType = inputEventType;
        }
    }
}