using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DLDReborn.DiscordBot
{
    //Вся магия тут
    public class CommandHandler : ICommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _provider;
        private readonly ILogger<CommandHandler> _logger;

        public CommandHandler(DiscordSocketClient client, CommandService commands, IConfiguration configuration, IServiceProvider provider, ILogger<CommandHandler> logger)
        {
            _client = client;
            _commands = commands;
            _configuration = configuration;
            _provider = provider;
            _logger = logger;

            _client.Ready += _client_Ready;
            _client.MessageReceived += _client_MessageReceived;
            _commands.CommandExecuted += _commands_CommandExecuted;
        }

        //Отправляем команду на испольнение
        private async Task _commands_CommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (command.IsSpecified && !result.IsSuccess)
            {
                await context.Channel.SendMessageAsync($"Твоя команда не прошла. \n Почему? \n Так вот почему: ```{result.Error}```");
                _logger.LogError(result.ErrorReason);
            }
        }

        //Тут мы мониторим все сообщения и фильтруем их для получения команды и также обрабатываем ошибки в случае чего
        private async Task _client_MessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            var prefix = _configuration["Discord:Prefix"].FirstOrDefault();

            if (msg == null || msg.Author.IsBot)
            {
                return;
            }

            var context = new SocketCommandContext(_client, msg);

            int pos = 0;
            if (msg.HasCharPrefix(prefix, ref pos) || msg.HasMentionPrefix(_client.CurrentUser, ref pos))
            {
                await _commands.ExecuteAsync(context, pos, _provider);
            }
        }

        //Просто приколдес ради того чтобы бахнуть уведомляшку про запуск. Пишет в логово ботов мое
        private async Task _client_Ready()
        {
            var chanelId = _configuration["Discord:Lair"];
            var ulongId = Convert.ToUInt64(chanelId);
            await (_client.GetChannel(ulongId) as SocketTextChannel).SendMessageAsync("Я готов. Можно начинать!");
            _logger.LogInformation($"Ready. Bot is {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}");
        }
    }
}
