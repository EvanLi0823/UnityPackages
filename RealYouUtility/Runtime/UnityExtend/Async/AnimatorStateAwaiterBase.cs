using System;
using System.Threading;
using UnityEngine;

namespace RealYou.Utility.AsyncOperation
{
    public class AnimatorStateAwaiterBase : StateMachineBehaviour, IAwaiter
    {
        public string Name;
        
        public bool IsDone { get; protected set; }
        
        public bool IsValid => this != null;
        
        public string Error { get; protected set; }
        
        public object Result { get;}
        
        public event Action<IAwaiter> OnCompleted;

        public void ResetState()
        {
            IsDone = false;
            OnCompleted = null;
            Error = null;
        }

        public void Cancel()
        {
            if(IsDone)
                return;

            Error = AwaiterError.Cancel;
            InvokeCompleted();
        }

        protected void InvokeCompleted()
        {
            if(IsDone)
                return;
            IsDone = true;
            var action = Interlocked.Exchange(ref OnCompleted, null);
            action?.Invoke(this);
        }

        private void OnDestroy()
        {
            InvokeCompleted();
        }
    }
}