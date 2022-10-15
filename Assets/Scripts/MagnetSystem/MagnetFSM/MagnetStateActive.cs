using FSM;

namespace MagnetSystem.MagnetFSM
{
    public class MagnetStateActive : FSMState<EMagnetState, MagnetFSMTransitionMessage>
    {
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Active;
        }
    }
}