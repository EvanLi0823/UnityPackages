using System;
using System.Runtime.CompilerServices;

namespace RealYou.Utility.AsyncOperation
{
    public class Awaiter : INotifyCompletion
    {
        private IAwaiter _operation;

        private Action _continuation;

        public bool IsCompleted => _operation == null || _operation.IsDone;

        public Awaiter(IAwaiter operation)
        {
            _operation = operation;
        }


        public object GetResult()
        {
            return _operation?.Result;
        }
        public void OnCompleted(Action continuation)
        {
            if (_operation == null)
            {
                continuation();
            }
            else
            {
                _continuation = continuation;
                _operation.OnCompleted += Next;
            }
        }

        private void Next(IAwaiter operation)
        {
//            if (operation != null && operation.IsValid)
            if (operation != null)
            {
                _continuation?.Invoke();
            }
        }
    }

    public static class AsyncOperationExtensions
    {
        public static Awaiter GetAwaiter(this IAwaiter operation)
        {
            return new Awaiter(operation);
        }
        
        public static Awaiter GetAwaiter(this IAsyncOperation operation)
        {
            return new Awaiter(operation);
        }
    }
}