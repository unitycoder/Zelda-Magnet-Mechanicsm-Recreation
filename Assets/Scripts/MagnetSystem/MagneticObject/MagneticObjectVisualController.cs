using Core.Common.Extensions;
using Core.ServiceSystem;
using DG.Tweening;
using UnityEngine;

namespace MagnetSystem
{
    public class MagneticObjectVisualController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer = null;

        [SerializeField] private MagneticObject _magneticObject = null;

        [SerializeField, ColorUsage(true)] private Color _highlightBaseColor = default;
        [SerializeField, ColorUsage(true, true)] private Color _highlightEmissionColor = default;

        [SerializeField, ColorUsage(true)] private Color _attachedBaseColor = default;
        [SerializeField, ColorUsage(true, true)] private Color _attachedEmissionColor = default;

        private MaterialPropertyBlock _mpb;
        
        private static readonly int _baseColorPropertyID = Shader.PropertyToID("_BaseColor");
        private static readonly int _emissionColorPropertyID = Shader.PropertyToID("_EmissionColor");

        private void Awake()
        {
            RegisterToMagnetFSM();
            
            SetColor(_highlightBaseColor.Alphaed(0), _highlightEmissionColor);
        }

        private void OnDestroy()
        {
            UnregisterFromMagnetFSM();
        }

        private void RegisterToMagnetFSM()
        {
            MagnetFSM magnetFsm = ServiceProvider.Get<MagnetFSM>();

            magnetFsm.OnEnterState += OnMagnetFSMEnterState;
        }

        private void UnregisterFromMagnetFSM()
        {
            MagnetFSM magnetFsm = ServiceProvider.Get<MagnetFSM>();

            magnetFsm.OnEnterState -= OnMagnetFSMEnterState;
        }

        private void OnMagnetFSMEnterState(EMagnetState magnetState)
        {
            if (magnetState == EMagnetState.Active)
            {
                OnMagnetActivated();
            }

            if (magnetState == EMagnetState.Idle)
            {
                OnMagnetDeactivated();
            }
        }

        private void OnMagnetActivated()
        {
            SetColor(_highlightBaseColor, _highlightEmissionColor);
            
            RegisterToMagneticObject();
        }

        private void OnMagnetDeactivated()
        {
            UnregisterFromMagneticObject();
            
            SetColor(_highlightBaseColor.Alphaed(0), _highlightEmissionColor);
        }

        private void RegisterToMagneticObject()
        {
            _magneticObject.OnAttached += OnMagneticObjectAttached;
            _magneticObject.OnDetached += OnMagneticObjectDetached;
        }

        private void UnregisterFromMagneticObject()
        {
            _magneticObject.OnAttached -= OnMagneticObjectAttached;
            _magneticObject.OnDetached -= OnMagneticObjectDetached;
        }

        private void OnMagneticObjectAttached()
        {
            SetColor(_attachedBaseColor, _attachedEmissionColor);
        }

        private void OnMagneticObjectDetached()
        {
            SetColor(_highlightBaseColor, _highlightEmissionColor);
        }

        private void SetColor(Color baseColor, Color emissionColor)
        {
            InitPropertyBlock();
            
            _mpb.SetColor(_baseColorPropertyID, baseColor);
            _mpb.SetColor(_emissionColorPropertyID, emissionColor);

            _meshRenderer.SetPropertyBlock(_mpb);
        }
        
        private void InitPropertyBlock()
        {
            if (_mpb == null)
            {
                _mpb = new MaterialPropertyBlock();
                
                _mpb.SetColor(_baseColorPropertyID, _meshRenderer.material.GetColor(_baseColorPropertyID));
                _mpb.SetColor(_emissionColorPropertyID, _meshRenderer.material.GetColor(_emissionColorPropertyID));
                
                _meshRenderer.SetPropertyBlock(_mpb);
            }
        }
    }
}