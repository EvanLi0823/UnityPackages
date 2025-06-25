using System;
using System.Threading;
using RealYou.Utility.LinkList;
using UnityEngine;

namespace RealYou.Utility.AsyncOperation
{
//    public abstract class AwaitAble : IAwaiter
//    {
//        public abstract bool IsDone { get; }
//        public abstract bool IsValid { get; }
//        public abstract string Error { get; }
//        public abstract object Result { get; }
//        
//        public event Action<IAwaiter> OnCompleted
//        {
//            add
//            {
//                _delegateList.Add(value);
//            }
//            remove
//            {
//                _delegateList.Remove(value);
//            }
//        }
//        
//        private DelegateList<IAwaiter> _delegateList;
//
//        private DelegateList<IAwaiter>[] _delegateListBuff;
//
//
//        protected AwaitAble()
//        {
//            _delegateListBuff = new DelegateList<IAwaiter>[]{
//                DelegateList<IAwaiter>.CreateWithGlobalCache(),
//                DelegateList<IAwaiter>.CreateWithGlobalCache()
//            };
//            _delegateList = _delegateListBuff[0];
//        }
//
//        protected void InvokeCompletionEvent()
//        {
//            var prev = _delegateList;
//
//            _delegateList = _delegateListBuff[1];
//            _delegateListBuff[1] = _delegateListBuff[0];
//            _delegateListBuff[0] = _delegateList;
//            
//            
//            prev.Invoke(this);
//            prev.Clear();
//
//        }
//
//    }

    public class AwaitAble : IAwaiter
    {
        public virtual bool IsDone { get; protected set; }
        public virtual bool IsValid => true;
        public virtual string Error { get; protected set; }
        public virtual object Result { get; protected set; }

        public event Action<IAwaiter> OnCompleted;

        protected void InvokeCompletionEvent()
        {
            if(IsDone)
                return;
            
            IsDone = true;
            var action = Interlocked.Exchange(ref OnCompleted, null);
            action?.Invoke(this);

        }

        protected virtual void OnCancel()
        {
            
        }

        public void SetError(string error)
        {
            Error = error;
            Cancel();
        }
        

        public void Cancel()
        {
            if(IsDone)
                return;

            if (string.IsNullOrEmpty(Error))
            {
                Error = AwaiterError.Cancel;
            }
            InvokeCompletionEvent();
            OnCancel();
        }
    }

    public class AwaitableMono : MonoBehaviour, IAwaiter
    {
        public virtual bool IsDone { get; protected set; }
        public virtual bool IsValid => true;
        public virtual string Error { get; protected set; }
        public virtual object Result { get; protected set; }

        public event Action<IAwaiter> OnCompleted;

        protected void InvokeCompletionEvent()
        {
            if(IsDone)
                return;
            
            IsDone = true;
            var action = Interlocked.Exchange(ref OnCompleted, null);
            action?.Invoke(this);

        }

        protected virtual void OnCancel()
        {
            
        }

        public void Cancel()
        {
            if(IsDone)
                return;

            Error = AwaiterError.Cancel;
            InvokeCompletionEvent();
            
            OnCancel();
        }
    }
    
}