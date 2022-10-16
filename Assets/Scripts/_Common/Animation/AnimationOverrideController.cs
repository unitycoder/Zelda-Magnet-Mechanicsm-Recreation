using System;
using UnityEngine;

namespace Common.Animation
{
    [Serializable]
    public class AnimationClipInfo
    {
        [field: SerializeField] public string StateName { get; private set; }
        
        [field: SerializeField] public AnimationClip Clip { get; private set; }
    }
    
    public class AnimationOverrideController : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController = null;

        private AnimatorOverrideController _animatorOverrideController;
        private AnimatorOverrideController _AnimatorOverrideController
        {
            get
            {
                if (_animatorOverrideController == null)
                {
                    InitOverrideController();
                }

                return _animatorOverrideController;
            }
        }

        public void UpdateAnimationClip(AnimationClipInfo info)
        {
            _AnimatorOverrideController[info.StateName] = info.Clip;
        }

        private void InitOverrideController()
        {
            _animatorOverrideController = new AnimatorOverrideController(_animationController.Animator.runtimeAnimatorController);
            
            _animationController.Animator.runtimeAnimatorController = _animatorOverrideController;
        }
    }
}