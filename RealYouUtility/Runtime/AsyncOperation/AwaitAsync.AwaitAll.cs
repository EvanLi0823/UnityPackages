using System;
using System.Threading;
using System.Threading.Tasks;

namespace RealYou.Utility.AsyncOperation
{
    public static partial class AwaitAsync
    {
        public static IAwaiter AwaitAll(params IAwaiter[] awaiter)
        {
            return new AwaitAllImp(awaiter);
        }

        private class AwaitAllImp : AwaitAble
        {
            private int _awaiterLength;

            private int _completeCount;
            
            public AwaitAllImp(params IAwaiter[] awaiters)
            {
                
                if (awaiters == null || awaiters.Length < 1)
                {
                    IsDone = true;
                    return;
                }

                IsDone = false;
                _completeCount = 0;
                _awaiterLength = awaiters.Length;

                for (int i = 0; i < _awaiterLength; i++)
                {
                    var awaiter = awaiters[i];

                    if (awaiter == null)
                    {
                        _completeCount++;
                    }
                    else if (awaiter.IsDone)
                    {
                        if (!string.IsNullOrEmpty(awaiter.Error))
                            Error = awaiter.Error;
                        
                        _completeCount++;
                    }
                    else
                    {
                        AwaitOne(awaiter);
                    }
                }

                TryCallContinue();
            }

            private async Task AwaitOne(IAwaiter awaiter)
            {
                await awaiter;
                if (!string.IsNullOrEmpty(awaiter.Error))
                    Error = awaiter.Error;
                
                Interlocked.Increment(ref _completeCount);
                
                TryCallContinue();
            }

            private void TryCallContinue()
            {
                if (_completeCount > _awaiterLength)
                {
                    Error =
                        $"the await count is error: complete count is {_completeCount},awaiter count is {_awaiterLength}";
                }
                
                if (_completeCount >= _awaiterLength)
                {
                   InvokeCompletionEvent();
                }
            }
        }
    }
}