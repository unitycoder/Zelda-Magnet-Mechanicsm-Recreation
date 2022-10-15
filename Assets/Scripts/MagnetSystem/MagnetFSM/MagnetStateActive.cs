﻿using FSM;
using Util;
using UnityEngine;
using System.Collections;

namespace MagnetSystem.MagnetFSM
{
    public class MagnetStateActive : FSMState<EMagnetState, MagnetFSMTransitionMessage>
    {
        [SerializeField] private MagnetFSM _magnetFSM = null;

        [SerializeField] private float _maxDistance = 5f;
        
        private IEnumerator _magneticObjectSearchRoutine;
        
        public override EMagnetState GetStateType()
        {
            return EMagnetState.Active;
        }

        protected override void EnterStateCustomActions(MagnetFSMTransitionMessage transitionMessage = null)
        {
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
            if (_magnetFSM.CurState.GetStateType() != GetStateType() && 
                Input.GetKey(Constants.MagnetActivationKeyCode))
            {
                _magnetFSM.TryChangeState(GetStateType());
            }
            
            if (_magnetFSM.CurState.GetStateType() == GetStateType() && 
                Input.GetKey(Constants.MagnetActivationKeyCode))
            {
                _magnetFSM.TryChangeState(EMagnetState.Idle);
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
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                IMagneticObject magneticObject = null;
                
                if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxDistance))
                {
                    magneticObject = hitInfo.transform.GetComponent<IMagneticObject>();
                }

                if (magneticObject != null)
                {
                    if (Input.GetMouseButton(Constants.MagnetObjectTriggerMouseButton))
                    {
                        _magnetFSM.TryChangeState(EMagnetState.Holding,
                            new MagnetFSMHoldingTransitionMessage(magneticObject));
                    }
                }

                yield return null;
            }
        }
    }
}