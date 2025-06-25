using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealYou.Utility.Command
{
//    public static class CommandAwait
//    {
//        public static AwaitAll WhenAll(object context, params CommandBase[] commands)
//        {
//            CommandQueue queue = CommandQueueMgr.GetQueue(commands[0].Layer);
//            AwaitAll all = new AwaitAll(context,queue);
//            all.AddRange(commands);
//
//            return all;
//        }
//        
//        /// <summary>
//        /// subCommand按照添加的顺序一个一个的执行.
//        /// </summary>
//        public class AwaitAll : CommandBase
//        {
//            private List<CommandBase> _commands;
//            
//            internal AwaitAll(object context, CommandQueue queue) : base(context, queue)
//            {
//                _commands = new List<CommandBase>();
//            }
//
//
//            public void Add(CommandBase command)
//            {
//                _commands.Add(command);
//            }
//
//            public void AddRange(IEnumerable<CommandBase>  command)
//            {
//                _commands.AddRange(command);
//            }
//            
//            public void Insert(int index, CommandBase command)
//            {
//                _commands.Insert(index,command);
//            }
//
//            protected override Task OnExecute()
//            {
//                throw new System.NotImplementedException();
//            }
//        }
//    }
}