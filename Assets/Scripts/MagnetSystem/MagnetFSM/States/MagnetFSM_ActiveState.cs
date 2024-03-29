﻿using Util;
using Core.FSM;
using UnityEngine;
using System.Collections;
using Core.CameraSystem;
using Core.ServiceSystem;

namespace MagnetSystem
{
    public class MagnetFSM_ActiveState : FSMState<EMagnetState, MagnetFSMTransitionMessage>
    {
        [SerializeField] private float _maxDistance = 5f;

        private IEnumerator _magneticObjectSearchRoutine;
        
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Active;
        }

        protected override void EnterStateCustomActions(MagnetFSMTransitionMessage transitionMessage = null)
        {
            ServiceProvider.Get<CameraManager>().SetTransition(Constants.AimCam);

            StartMagneticObjectSearch();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            StopMagneticObjectSearch();
            
            base.ExitStateCustomActions();
        }

        private void Update()
        {
            if (FSM.CurState.GetStateType() != GetStateType() && 
                Input.GetKeyUp(Constants.MagnetActivationKeyCode))
            {
                FSM.TryChangeState(GetStateType());
                
                return;
            }
            
            if (FSM.CurState.GetStateType() == GetStateType() && 
                Input.GetKeyUp(Constants.MagnetActivationKeyCode))
            {
                FSM.TryChangeState(EMagnetState.Idle);
                
                return;
            }
        }

        private void OnDestroy()
        {
            StopMagneticObjectSearch();
        }

        private void StartMagneticObjectSearch()
        {
            StopMagneticObjectSearch();

            _magneticObjectSearchRoutine = MagneticObjectSearchProgress();

            StartCoroutine(_magneticObjectSearchRoutine);
        }

        private void StopMagneticObjectSearch()
        {
            if (_magneticObjectSearchRoutine != null)
            {
                StopCoroutine(_magneticObjectSearchRoutine);
            }
        }

        private IEnumerator MagneticObjectSearchProgress()
        {
            while (true)
            {
                Ray ray = Camera.main.ViewportPointToRay(Constants.AimPosition);

                IMagneticObject magneticObject = null;
                
                if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxDistance))
                {
                    magneticObject = hitInfo.transform.GetComponent<IMagneticObject>();
                }

                if (magneticObject != null)
                {
                    if (Input.GetMouseButtonUp(Constants.MagnetObjectTriggerMouseButton))
                    {
                        FSM.TryChangeState(EMagnetState.Holding,
                            new MagnetFSMHoldingTransitionMessage(magneticObject));
                    }
                }

                yield return null;
            }
        }
    }
}