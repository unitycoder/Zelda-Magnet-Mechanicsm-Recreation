using Core.FSM;
using UnityEngine;

namespace MagnetSystem.MagnetFSM
{
    public class MagnetFSMHoldingTransitionMessage : MagnetFSMTransitionMessage
    {
        public IMagneticObject MagneticObject { get; }

        public MagnetFSMHoldingTransitionMessage(IMagneticObject magneticObject)
        {
            MagneticObject = magneticObject;
        }
    }
    
    public class MagnetFSM_HoldingState : FSMState<EMagnetState, MagnetFSMTransitionMessage>
    {
        [SerializeField] private Magnet _magnet = null;
        
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Holding;
        }
        
        protected override void EnterStateCustomActions(MagnetFSMTransitionMessage transitionMessage = null)
        {
            MagnetFSMHoldingTransitionMessage holdingTransitionMessage =
                (MagnetFSMHoldingTransitionMessage) transitionMessage;
            
            if (transitionMessage == null)
            {
                Debug.LogError($"Holding Message null !..");
                
                return;
            }
            
            _magnet.AttachMagneticObject(holdingTransitionMessage.MagneticObject);
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _magnet.DeattachMagneticObject();
            
            base.ExitStateCustomActions();
        }
    }
}