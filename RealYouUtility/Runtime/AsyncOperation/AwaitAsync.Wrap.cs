using System.Runtime.CompilerServices;

namespace RealYou.Utility.AsyncOperation
{
    public static partial class AwaitAsync
    {
        public static IAwaiter WrapTask(TaskAwaiter task)
        {
            return new WrapTaskImpl(task);
            
        }

        private class WrapTaskImpl : AwaitAble
        {   
            public WrapTaskImpl(TaskAwaiter task)
            {
                if (task.IsCompleted)
                {
                    InvokeCompletionEvent();
                }
                else
                {
                    task.OnCompleted(InvokeCompletionEvent);
                }
            }
        }
    }
}