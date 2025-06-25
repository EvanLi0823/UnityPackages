using UnityEngine;

namespace RealYou.Utility.AsyncOperation
{

    public class AnimatorStateExitAwaiter : AnimatorStateAwaiterBase
    { 
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            InvokeCompleted();
        }
    }
}