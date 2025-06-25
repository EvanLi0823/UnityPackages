using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace RealYou.Utility.AsyncOperation.CompilerServices
{
    public struct AsyncAwaitMethodBuilder
    {
        private SimpleAsyncOperation _task;

        private Action _moveNext;
        
        [DebuggerHidden]
        public static AsyncAwaitMethodBuilder Create()
        {
            return new AsyncAwaitMethodBuilder();
        }

        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine?.MoveNext();
        }

        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            
        }

        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (_task == null)
            {
                _task = new SimpleAsyncOperation();
            }
            _task.SetError(exception.ToString());
        }

        [DebuggerHidden]
        public void SetResult()
        {
            if (_task == null)
            {
                _task = new SimpleAsyncOperation();
            }
            
            _task.Done();
        }

        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (_moveNext == null)
            {
                if (_task == null)
                {
                    _task = new SimpleAsyncOperation();
                }

                var runner = new MoveNextRunner<TStateMachine>();
                runner.StateMachine = stateMachine;
                _moveNext = runner.Run;
            }
            awaiter.OnCompleted(_moveNext);
        }

        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            AwaitOnCompleted(ref awaiter, ref stateMachine);
        }

        [DebuggerHidden]
        public IAwaiter Task
        {
            get
            {
                if (_task != null)
                    return _task;
                
                _task = new SimpleAsyncOperation();
                if (_moveNext == null)
                {
                     _task.Done();
                }

                
                return _task;
            }
        }
    }
    
    public struct AsyncAwaitMethodBuilder<T>
    {
        private T _result;
        
        private AsyncCompletionSource<T> _task;

        private Action _moveNext;
        
        [DebuggerHidden]
        public static AsyncAwaitMethodBuilder<T> Create()
        {
            return new AsyncAwaitMethodBuilder<T>();
        }

        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine?.MoveNext();
        }

        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            
        }

        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (_task == null)
            {
                _task = new AsyncCompletionSource<T>();
            }
            _task.SetError(exception.ToString());
        }

        [DebuggerHidden]
        public void SetResult(T result)
        {
            if (_task == null)
            {
                _task = new AsyncCompletionSource<T>();
            }

            _result = result;
            _task.Done(result);
        }

        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (_moveNext == null)
            {
                if (_task == null)
                {
                    _task = new AsyncCompletionSource<T>();
                }

                var runner = new MoveNextRunner<TStateMachine>();
                runner.StateMachine = stateMachine;
                _moveNext = runner.Run;
            }
            awaiter.OnCompleted(_moveNext);
        }

        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            AwaitOnCompleted(ref awaiter, ref stateMachine);
        }

        [DebuggerHidden]
        public IAwaiter<T> Task
        {
            get
            {
                if (_task != null)
                    return _task;
                
                _task = new AsyncCompletionSource<T>();
                
                if (_moveNext == null)
                {
                    _task.Done(_result);
                }

                return _task;
            }
        }
    }
}