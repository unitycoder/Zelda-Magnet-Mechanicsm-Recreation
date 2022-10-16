using UnityEngine;

namespace Common.Animation
{
    public static class AnimatorExtensions
    {
        public static T TryGetBehaviour<T>(this Animator animator, string stateFullPath, int layerIndex = 0)
        where T : StateMachineBehaviour
        {
            StateMachineBehaviour[] behaviours =
                animator.GetBehaviours(Animator.StringToHash(stateFullPath), layerIndex);

            foreach (StateMachineBehaviour behaviour in behaviours)
            {
                if (behaviour is T targetBehaviour)
                {
                    return targetBehaviour;
                }
            }

            return null;
        }
    }
}