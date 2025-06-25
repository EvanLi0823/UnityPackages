using System;
using System.Threading;

namespace RealYou.Utility.AsyncOperation
{
    public class SimpleAsyncOperation : AwaitAble, IAsyncOperation
    {
        public bool MoveNext()
        {
            return !IsDone;
        }

        public void Reset()
        {
            Error = string.Empty;
            IsDone = false;
        }

        public object Current { get; protected set; }

        public void Done()
        {
            InvokeCompletionEvent();
        }
    }

    public class AsyncCompletionSource<T> : AwaitAble, IAsyncOperation,IAwaiter<T>
    {
        public static AsyncCompletionSource<T> NullObject { get; }
        public bool IsTimeout { get; private set; }
        
        public  new T Result { get; protected set; }

        private IAwaiter _timeout;

        static AsyncCompletionSource()
        {
            NullObject = new AsyncCompletionSource<T>();
            NullObject.SetError("null");
        }

        public bool MoveNext()
        {
            return !IsDone;
        }

        public void Reset()
        {
            Result = default(T);
            IsTimeout = false;
            IsDone = false;
            _timeout?.Cancel();
            _timeout = null;
            Error = string.Empty;
        }

        public async void Timeout(float timeout)
        {
            if (!(timeout > 0))
            {
                return;
            }

            _timeout = AwaitAsync.Delay(timeout);
            await _timeout;
            
            if(AwaiterError.Cancel.Equals(_timeout.Error))
                return;
            
            if(IsDone)
                return;
            
            IsTimeout = true;
            SetError("Timed Out");
        }

        public object Current { get; protected set; }

        public void Done(T result)
        {
            if(IsDone)
                return;
            _timeout?.Cancel();
            Result = result;
            InvokeCompletionEvent();
            
        }
    }

    public class NullAsyncOperation : IAsyncOperation
    {
        public static NullAsyncOperation DefaultObject { get; } = new NullAsyncOperation();

        public NullAsyncOperation()
        {
            IsDone = true;
            Error = "NullAsyncOperation";
        }
        
        public bool MoveNext()
        {
            return !IsDone;
        }

        public void Reset()
        {
           
        }

        void IAwaiter.Cancel()
        {
            
        }

        public object Current { get;}
        public bool IsDone { get; }
        public bool IsValid { get;  }
        public string Error { get;}
        public object Result { get;}
        public event Action<IAwaiter> OnCompleted;
    }
    
    public class NullAsyncOperation<T> : NullAsyncOperation,IAsyncOperation<T>
    {
        public static NullAsyncOperation<T> DefaultObject { get; } = new NullAsyncOperation<T>();
        public T Result { get; set; }
    }
    
    public class CompletedAsyncOperation : IAsyncOperation
    {

        public static CompletedAsyncOperation DefaultObject { get; } = new CompletedAsyncOperation();

        public CompletedAsyncOperation()
        {
            IsDone = true;
            IsValid = true;
        }
        
        public bool MoveNext()
        {
            return !IsDone;
        }

        public void Reset()
        {
            
        }
        
        void IAwaiter.Cancel()
        {
            
        }

        public object Current { get;}
        public bool IsDone { get; }
        public bool IsValid { get;  }
        public string Error { get;}
        public object Result { get;}
        public event Action<IAwaiter> OnCompleted;
    }

    public class CompletedAsyncOperation<T> : CompletedAsyncOperation,IAsyncOperation<T>,IAwaiter<T>
    {
        public static CompletedAsyncOperation <T>DefaultObject { get; } = new CompletedAsyncOperation<T>();
        public T Result { get; set; }
    }
}