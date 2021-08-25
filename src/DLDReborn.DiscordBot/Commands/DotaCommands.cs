using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DLDReborn.DiscordBot.Commands
{
    public class DotaCommands : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<MyCommands> _logger;
        private readonly IConfiguration _configuration;

        public DotaCommands(ILogger<MyCommands> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Command("ласткатка")]
        public async Task LastMatchCommand()
        {
            await Context.Channel.SendMessageAsync($"пишу ласт катку", false, null, null, null, Context.Message.Reference);
        }

        [Command("дотастатистика")]
        public async Task DotaStatistickCommand()
        {
            await Context.Channel.SendMessageAsync($"пишу стату в дотке", false, null, null, null, Context.Message.Reference);
        }
    }
}
