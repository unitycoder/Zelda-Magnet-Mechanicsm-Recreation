using Core.CameraSystem;
using Core.Common;
using Core.FSM;
using Core.ServiceSystem;
using UnityEngine;
using Util;

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

        [SerializeField] private RotationBehaviour _rotationBehaviour = null;
        
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

            LookTowardsToObject(holdingTransitionMessage.MagneticObject.Transform.position);
            
            ServiceProvider.Get<CameraManager>().SetTransition(Constants.AimCam);

            _rotationBehaviour.LockRotation(true);

            _magnet.AttachMagneticObject(holdingTransitionMessage.MagneticObject);
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _rotationBehaviour.LockRotation(false);
            
            _magnet.DeattachMagneticObject();
            
            base.ExitStateCustomActions();
        }

        private void LookTowardsToObject(Vector3 objectPos)
        {
            Vector3 direction = objectPos - _rotationBehaviour.GetRotationBody().position;
            direction.y = 0;
            
            _rotationBehaviour.Rotate(direction.normalized, false);
        }
    }
}