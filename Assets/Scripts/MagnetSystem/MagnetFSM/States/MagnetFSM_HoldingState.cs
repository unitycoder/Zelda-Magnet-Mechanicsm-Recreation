using CharacterSystem;
using Core.Common;
using Core.FSM;
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

        [SerializeField] private RotationBehaviour_LookDir _rotationBehaviour = null;

        [SerializeField] private CharacterCameraController _cameraController = null;
        
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Holding;
        }
        
        protected override async void EnterStateCustomActions(MagnetFSMTransitionMessage transitionMessage = null)
        {
            MagnetFSMHoldingTransitionMessage holdingTransitionMessage =
                (MagnetFSMHoldingTransitionMessage) transitionMessage;
            
            if (transitionMessage == null)
            {
                Debug.LogError($"Holding Message null !..");
                
                return;
            }

            LookTowardsToObject(holdingTransitionMessage.MagneticObject.Transform.position);

            await _magnet.AttachMagneticObject(holdingTransitionMessage.MagneticObject);
            
            _cameraController.EnableCharacterRotation(true);
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _cameraController.EnableCharacterRotation(false);
         
            _rotationBehaviour.LockRotation(false);
            
            _magnet.DeattachMagneticObject();
            
            base.ExitStateCustomActions();
        }
        
        private void Update()
        {
            if (FSM.CurState.GetStateType() == GetStateType() && 
                Input.GetKeyUp(Constants.MagnetActivationKeyCode))
            {
                FSM.TryChangeState(EMagnetState.Idle);
            }
        }

        private void LookTowardsToObject(Vector3 objectPos)
        {
            Vector3 direction = objectPos - _rotationBehaviour.GetRotationBody().position;
            direction.y = 0;
            
            _rotationBehaviour.Rotate(direction.normalized, false);
            
            _rotationBehaviour.LockRotation(true);
        }
    }
}