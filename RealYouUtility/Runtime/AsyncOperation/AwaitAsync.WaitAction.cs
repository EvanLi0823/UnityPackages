
using System;
using RealYou.Utility.BFException;

namespace RealYou.Utility.AsyncOperation
{
    public static partial class AwaitAsync
    {
        public static IAwaiter WaitAction(ref Action action)
        {
            if(action == null)
                return NullAsyncOperation.DefaultObject;
            
            return new WaitActionImpl(ref action);
            
        }

        private class WaitActionImpl : AwaitAble
        {   
            public WaitActionImpl(ref Action action)
            {
                if (action == null)
                    throw new ParamsNullException();

                action += Invoke;
            }

            private void Invoke()
            {
                if(IsDone)
                    return;
                
                InvokeCompletionEvent();
            }
        }
    }
}