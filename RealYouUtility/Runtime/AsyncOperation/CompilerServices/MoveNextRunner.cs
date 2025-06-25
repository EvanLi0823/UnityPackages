using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace RealYou.Utility.AsyncOperation.CompilerServices
{
    internal class MoveNextRunner<TStateMachine>
        where TStateMachine : IAsyncStateMachine
    {
        public TStateMachine StateMachine;

        [DebuggerHidden]
        public void Run()
        {
            StateMachine.MoveNext();
        }
    }
}