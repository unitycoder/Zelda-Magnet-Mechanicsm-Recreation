using FSM;

namespace MagnetSystem.MagnetFSM
{
    public class MagnetStateHolding : FSMState<EMagnetState, MagnetFSMTransitionMessage>
    {
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Holding;
        }
    }
}