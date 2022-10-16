using System;
using UnityEngine;

namespace Common.Animation
{
    public class AnimationEventBehaviour : StateMachineBehaviour
    {
        public Action<float> OnStateMoved { get; set; }
        
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float normalizedTime = Mathf.Repeat(stateInfo.normalizedTime, 1f);
            
            OnStateMoved?.Invoke(normalizedTime);

            base.OnStateMove(animator, stateInfo, layerIndex);
        }
    }
}
