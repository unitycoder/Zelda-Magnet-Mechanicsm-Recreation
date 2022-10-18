using Core.FSM;

namespace MagnetSystem.MagnetFSM
{
    public class MagnetFSM_IdleState : FSMState<EMagnetState, MagnetFSMTransitionMessage>
    {
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Idle;
        }
    }
}