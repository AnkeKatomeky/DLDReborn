using System.Threading.Tasks;

namespace DLDReborn.DiscordBot
{
    public interface IEntryPoint
    {
        Task RunAsync();
    }
}