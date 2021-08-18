using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace sui_hiring_bot
{
    class Program
    {
        private DiscordSocketClient _client;
        private ulong _channelId;
        private string? _token;
        private readonly Processing _obj = new();

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _channelId = Convert.ToUInt64(Environment.GetEnvironmentVariable("CHANNEL_ID"));
            _client.MessageReceived += MessageReceived;
            _token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
        private async Task MessageReceived(SocketMessage message)
        {
            if (_client.GetChannel(_channelId) == message.Channel && message.Content.Contains("!hr start"))
            {
                _obj.ProcessControl(message);
            }
            if (_client.GetChannel(_channelId) == message.Channel && message.Content.Contains("!hr stop"))
            {
                _obj.StopControl(message);
            }
            if (_client.GetChannel(_channelId) == message.Channel && message.Content.Contains("!hr status"))
            {
                //TODO:
                //Processing.StatusControl(message);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}