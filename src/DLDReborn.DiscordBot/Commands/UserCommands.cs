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
    class UserCommands : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<MyCommands> _logger;
        private readonly IConfiguration _configuration;

        public UserCommands(ILogger<MyCommands> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Command("что")]
        public async Task WhatCommand()
        {
            await Context.Channel.SendMessageAsync($"пишу инфу о то что я умеею", false, null, null, null, Context.Message.Reference);
        }

        [Command("привет")]
        public async Task HelloCommand()
        {
            await Context.Channel.SendMessageAsync($"пишу привет и запоминаю тебя", false, null, null, null, Context.Message.Reference);
        }
    }
}
