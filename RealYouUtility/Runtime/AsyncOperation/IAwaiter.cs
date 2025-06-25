using System;
using System.Runtime.CompilerServices;
using RealYou.Utility.AsyncOperation.CompilerServices;

namespace RealYou.Utility.AsyncOperation
{
    [AsyncMethodBuilder(typeof(AsyncAwaitMethodBuilder))]
    public interface IAwaiter
    {
        bool IsDone { get;}

        bool IsValid { get; }
        
        string Error { get; }

        object Result { get; }

        event Action<IAwaiter> OnCompleted;

        void Cancel();
    }
    
    [AsyncMethodBuilder(typeof(AsyncAwaitMethodBuilder<>))]
    public interface IAwaiter<T> : IAwaiter
    {
        new T Result { get; }
    }
    
    public static class AwaiterError
    {
        public const string Cancel = "by cancel";
    }
}