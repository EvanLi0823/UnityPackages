using System.Threading.Tasks;

namespace RealYou.Utility.Command
{
    internal interface ICommand
    {
        Task Execute();

        void Cancel();

        Status Status { get; }
    }
}