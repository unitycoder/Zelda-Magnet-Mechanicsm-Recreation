using Core.CameraSystem;
using Core.FSM;
using Core.ServiceSystem;
using Util;

namespace MagnetSystem
{
    public class MagnetFSM_IdleState : FSMState<EMagnetState, MagnetFSMTransitionMessage>
    {
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Idle;
        }

        protected override void EnterStateCustomActions(MagnetFSMTransitionMessage transitionMessage = null)
        {
            ServiceProvider.Get<CameraManager>().SetTransition(Constants.DefaultCamera);
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}