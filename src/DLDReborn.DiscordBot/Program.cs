using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DLDReborn.DiscordBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((ctx, services) =>
                {
                    services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig()
                    {
                        MessageCacheSize = 1000
                    }));
                    services.AddSingleton(new CommandService(new CommandServiceConfig()
                    {
                        DefaultRunMode = RunMode.Async,
                        CaseSensitiveCommands = false
                    }));
                    services.AddSingleton<ICommandHandler, CommandHandler>();
                    services.AddHostedService<EntryPoint>();
                })
                .Build();

            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}
