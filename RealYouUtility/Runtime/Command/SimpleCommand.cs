
using System;
using System.Collections;
using System.Threading.Tasks;

namespace RealYou.Utility.Command
{
    public class SimpleCommand : CommandBase<object>
    {
        public SimpleCommand() : this(null, null)
        {
            
        }

        public SimpleCommand(CommandQueue queue) : this(null, queue)
        {
            
        }
        public SimpleCommand(object context) : this(context, null)
        {
            
        }
        public SimpleCommand(object context,CommandQueue queue):base(context,queue)
        {
            
        }

        protected  override Task OnExecute()
        {
//            await Task.Delay(TimeSpan.FromSeconds(1));
            return null;
        }
    }
}