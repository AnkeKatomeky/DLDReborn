using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DLDReborn.DiscordBot
{
    public class EntryPoint : IHostedService, IDisposable
    {
        private readonly ILogger<EntryPoint> _logger;
        private readonly IConfiguration _config;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _provider;
        private readonly ICommandHandler _handler;

        public EntryPoint(ILogger<EntryPoint> logger, IConfiguration config, DiscordSocketClient client, CommandService commands, IServiceProvider provider, ICommandHandler handler)
        {
            _logger = logger;
            _config = config;
            _client = client;
            _commands = commands;
            _provider = provider;
            _handler = handler;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Begin");
            var token = _config["Discord:Token"];
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Токен пустой. Вставь токен бота в конфигурацию.");
                return;
            }

            //Фулл запуск
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
            _logger.LogInformation("Begin Successful");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Ending");

            foreach (var item in _commands.Modules)
            {
                await _commands.RemoveModuleAsync(item);
            }
            
            await _client.LogoutAsync();
            await _client.StopAsync();
            _logger.LogInformation("Ending Successful");

        }

        public void Dispose()
        {
            _client?.Dispose();
            ((IDisposable) _commands)?.Dispose();
        }
    }
}
