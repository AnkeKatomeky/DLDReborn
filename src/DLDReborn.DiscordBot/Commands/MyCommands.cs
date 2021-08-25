using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DLDReborn.DiscordBot.Commands
{
    public class MyCommands : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<MyCommands> _logger;
        private readonly IConfiguration _configuration;

        public MyCommands(ILogger<MyCommands> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Command("пинг")]
        public async Task PingCommand()
        {
            await Context.Channel.SendMessageAsync($"понг");
        }

        [Command("пинг")]
        public async Task PingCommand(string echo)
        {
            await Context.Channel.SendMessageAsync($"понг {echo}");
        }

        public async Task InfoCommand()
        {
            //плашка прикольнаяд
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                .WithDescription("Чекни че про тебя написано))")
                .WithColor(new Color(50, 0, 0))
                .AddField($"Твой Ид:", Context.User.Id)
                .AddField("Твой ключ:", Context.User.Discriminator)
                .AddField("Ты создан:", Context.User.CreatedAt.ToString("f"))
                .WithCurrentTimestamp();
            var embet = builder.Build();
            await Context.Channel.SendMessageAsync("Kekus", false, embet);

            //напрямую челу
            var dmc = Context.User.GetOrCreateDMChannelAsync();
            await dmc.Result.SendMessageAsync(null, false, embet);
        }
    }
}
