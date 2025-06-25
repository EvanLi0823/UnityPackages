using UnityEngine;
using Logger = RealYou.Utility.Log.Logger;

namespace RealYou.Utility.AsyncOperation
{
    public static class AnimatorExtensions
    {
        private const float _defaultDelay = 1.0f;
        
        public static IAwaiter PlayAsync(this Animator animator, string stateName)
        {
            if (string.IsNullOrEmpty(stateName) || animator == null)
            {
                Logger.GlobalLogger.LogExceptionFormat("PlayAsync stateName:{0}, animator is null",stateName, animator == null);
                return NullAsyncOperation.DefaultObject;
            }

            return animator.PlayAsync(Animator.StringToHash(stateName),stateName);
        }
        
        public static IAwaiter PlayAsync(this Animator animator, int stateNameHash, string awaiterName = null)
        {  
            if(animator == null)
            {
                Logger.GlobalLogger.LogException("PlayAsync animator is null");
                return NullAsyncOperation.DefaultObject;
            }
            
            animator.Play(stateNameHash);
            
            var awaiters = animator.GetBehaviours<AnimatorStateAwaiterBase>();
            if (awaiters == null || awaiters.Length <= 0)
            {
                return AwaitAsync.Delay(_defaultDelay);
            }

            AnimatorStateAwaiterBase awaiter = null;
                
            if (string.IsNullOrEmpty(awaiterName))
            {
                awaiter = awaiters[0];
            }
            else
            {
                AnimatorStateAwaiterBase temp;
                AnimatorStateAwaiterBase anyAwaiter = null;
                for (int i = 0; i < awaiters.Length; i++)
                {
                    temp = awaiters[i];
                    if (string.IsNullOrEmpty(temp.Name))
                    {
                        anyAwaiter = temp;
                    }
                    
                    if (awaiterName.Equals(temp.Name))
                    {
                        awaiter = awaiters[i];
                        break;
                    }
                }

                if (awaiter == null)
                {
                    awaiter = anyAwaiter;
                }
            }

            if (awaiter != null)
            {
                awaiter.ResetState();
                return awaiter;
            }

            return AwaitAsync.Delay(_defaultDelay);
        }
    }
}