using UnityEngine;

namespace RealYou.Utility.AsyncOperation
{
    public class AnimatorStateEnterAwaiter : AnimatorStateAwaiterBase
    { 

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            InvokeCompleted();
        }
    }
}