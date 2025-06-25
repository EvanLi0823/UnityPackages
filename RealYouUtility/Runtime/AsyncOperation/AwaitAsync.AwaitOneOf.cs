using System.Threading.Tasks;

namespace RealYou.Utility.AsyncOperation
{
    public static partial class AwaitAsync
    {
        public static IAwaiter AwaitOneOf(params IAwaiter[] awaiter)
        {
            return new AwaitOneOfImp(awaiter);
        }
        
        private class AwaitOneOfImp : AwaitAble
        {
            public AwaitOneOfImp(params IAwaiter[] awaiters)
            {
                
                if (awaiters == null || awaiters.Length < 1)
                {
                    IsDone = true;
                    return;
                }

                IsDone = false;

                for (int i = 0; i < awaiters.Length; i++)
                {
                    var awaiter = awaiters[i];
                    
                    if (awaiter.IsDone)
                    {
                        if (!string.IsNullOrEmpty(awaiter.Error))
                            Error = awaiter.Error;

                        InvokeCompletionEvent();
                        break;
                    }
                    else
                    {
                        AwaitOne(awaiter);
                    }
                }
            }

            private async Task AwaitOne(IAwaiter awaiter)
            {
                await awaiter;
                if (!string.IsNullOrEmpty(awaiter.Error))
                    Error = awaiter.Error;
                
                InvokeCompletionEvent();
            }
        }
    }
}