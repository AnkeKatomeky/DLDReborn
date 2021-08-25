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
    public class InfoCommands : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<MyCommands> _logger;
        private readonly IConfiguration _configuration;

        public InfoCommands(ILogger<MyCommands> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Command("погода")]
        public async Task WeatherCommand()
        {
            await Context.Channel.SendMessageAsync($"пишу погоду", false, null, null, null, Context.Message.Reference);
        }

        [Command("крипта")]
        public async Task CryptocyrencyCommand()
        {
            await Context.Channel.SendMessageAsync($"пишу крипту", false, null, null, null, Context.Message.Reference);
        }
    }
}