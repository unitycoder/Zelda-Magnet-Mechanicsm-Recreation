using UnityEngine;

namespace Common.Animation
{
    public class AnimationController : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }

        public void CrossFadeIntoState(string stateName, float duration = 0.25f)
        {
            Animator.CrossFadeInFixedTime(stateName, duration, 0);
        }

        public void SetParameter(string paramName, float value)
        {
            Animator.SetFloat(paramName, value);
        }
    }
}