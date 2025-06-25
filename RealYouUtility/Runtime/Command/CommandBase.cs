using System;
using System.Collections;
using System.Threading.Tasks;
using RealYou.Utility.AsyncOperation;

namespace RealYou.Utility.Command
{
    public abstract class CommandBase<T> : IAsyncOperation<T>,ICommand
    {
        #region property and field
        
        public bool IsDone { get; protected set; }
        
        public object Current { get; protected set; }

        public string Error { get; protected set; }

        public Status Status { get; private set; }

        object IAwaiter.Result => Result;
        
        public virtual T Result { get; protected set; }

        public bool IsValid => _context != null && _context.IsAlive && _context.Target != null;

        public event Action<IAwaiter> OnCompleted;
        
        protected WeakReference _context;

        protected CommandQueue _queue;

        protected virtual int Layer => CommandQueueLayer.Normal;

        #endregion

        #region construct
        
        protected CommandBase(object context,CommandQueue queue)
        {
            if (context == null)
            {
                _context = new WeakReference(this);
            }
            else
            {
                _context = new WeakReference(context);
            }

            _queue = queue;
            Status = Status.Unknow;
        }

        #endregion
        
        #region IEnumerator

        void IEnumerator.Reset()
        {
            IsDone = false;
        }

        bool IEnumerator.MoveNext()
        {
            return !IsDone;
        }

        #endregion

        #region ICommand

        async Task ICommand.Execute()
        {
            if (IsDone || Status == Status.Running)
            {
                return;
            }

            Status = Status.Running;
            
            Task task = OnExecute();
            if (task != null)
            {
                await task;
            }
            
            if(IsDone)
                return;
            
            IsDone = true;
            InvokeCompletionEvent();
        }

        #endregion

        #region method

        public IAsyncOperation<T> Start()
        {
            if(Status != Status.Unknow)
                throw  new Exception("the command has start");

            Status = Status.Pending;
            
            if (_queue == null)
            {
                _queue = CommandQueueMgr.GetQueue(Layer);
            }
            _queue.Add(this);

            return this;
        }

        public void Cancel()
        {
            if(IsDone)
                return;
            
            IsDone = true;
            Status = Status.Canceled;
            Error = "cancel by user";
            OnCancel();
            InvokeCompletionEvent();
            _context = null;
        }

        #endregion

        protected void InvokeCompletionEvent()
        {
            if (Status != Status.Canceled)
            {
                Status = string.IsNullOrEmpty(Error) ? Status.Failed : Status.Success;
            }
            
            OnCompleted?.Invoke(this);
            OnCompleted = null;
        }

        protected virtual void OnCancel()
        {
            
        }

        protected object GetContext()
        {
            if (_context == null)
                return null;
            
            return _context.IsAlive?_context.Target : null;
        }

        #region abstract
        
        protected abstract Task OnExecute();

        #endregion
    }


    public abstract class CommandBase : CommandBase<object>
    {
        protected CommandBase(object context, CommandQueue queue) : base(context, queue)
        {
            
        }
    }
}