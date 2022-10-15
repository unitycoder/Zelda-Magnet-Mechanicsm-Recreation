using FSM;
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
    
    public class MagnetStateHolding : FSMState<EMagnetState, MagnetFSMHoldingTransitionMessage>
    {
        [SerializeField] private Magnet _magnet = null;
        
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Holding;
        }
        
        protected override void EnterStateCustomActions(MagnetFSMHoldingTransitionMessage transitionMessage = null)
        {
            if (transitionMessage == null)
            {
                Debug.LogError($"Holding Message null !..");
                
                return;
            }
            
            _magnet.AttachMagneticObject(transitionMessage.MagneticObject);
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _magnet.DeattachMagneticObject();
            
            base.ExitStateCustomActions();
        }
    }
}