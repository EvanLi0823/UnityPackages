using System;
using System.Collections;
using UnityEngine.Events;

namespace RealYou.Utility.AsyncOperation
{
    public interface IAsyncOperation : IEnumerator , IAwaiter
    {
        
    }

    public interface IAsyncOperation<T> : IAsyncOperation
    {
         new T Result { get; }
    }
}