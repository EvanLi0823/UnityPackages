using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealYou.Utility.Command
{
    public class CommandQueue
    {
        private Queue<ICommand> _commands;

        private List<ICommand> _running;

        private int _jobs;

        private int _capacity;

        private Action<Exception> _OnUnobservedException;
        
        internal CommandQueue() : this(128, 1,null)
        {
            
        }

        internal CommandQueue(Action<Exception> unobservedException) : this(128, 1,unobservedException)
        {
            
        }
        internal CommandQueue(int capacity,int jobs,Action<Exception> unobservedException)
        {
            _jobs = jobs;
            _capacity = capacity;
            
            _commands = new Queue<ICommand>(capacity);
            _running = new List<ICommand>(_jobs);
            
            _OnUnobservedException = unobservedException;
        }

        public void CancelAll()
        {
            foreach (var command in _running)
            {
                command.Cancel();
            }

            foreach (var command in _commands)
            {
                command.Cancel();
            }
            
            Clear();
        }

        internal void Add(ICommand command)
        {
            _commands.Enqueue(command);
            Execute();
        }

        private void Clear()
        {
            _commands.Clear();
            _running.Clear();
        }

        private void Execute()
        {
            ICommand command;
            for (int i = 0, max = Math.Min(_jobs - _running.Count, _commands.Count); i < max; i++)
            {
                command = _commands.Dequeue();
                _running.Add(command);
                Execute(command);
            }
        }

        private async void Execute(ICommand command)
        {
            try
            {
                Task task = command.Execute();
                if(task != null)
                    await task;
            }
            catch (Exception e)
            {
               _OnUnobservedException?.Invoke(e);
            }
            
            _running.Remove(command);
            Execute();
        }
    }
}