using FSM;

namespace MagnetSystem.MagnetFSM
{
    public class MagnetStateIdle : FSMState<EMagnetState, MagnetFSMTransitionMessage>
    {
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Idle;
        }
    }
}