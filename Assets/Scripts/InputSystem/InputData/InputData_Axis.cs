namespace InputSystem
{
    public enum EAxisInputState
    {
        None = 0,
        Started = 1,
        Continuing = 2,
        Finished = 3
    }
    
    public class InputData_Axis : InputData
    {
        public float Amount { get; }

        public EAxisInputState InputState { get; } = EAxisInputState.None;

        public InputData_Axis(EInputEvent inputEventType, float amount, EAxisInputState inputState) 
            : base(inputEventType)
        {
            Amount = amount;

            InputState = inputState;
        }
    }
}