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
            if (_client.GetChannel(_channelId) == message.Channel && message.Content.Contains("!scan"))
            {
                //String[] ships = new String[] { "Hulk" };
                var searchResult = ZkillboardScanShips.ScanForShips();
                /*foreach (var pilot in searchResult)
                {
                    message.Channel.SendMessageAsync($"Pilot Name: {pilot.Item1} \n System: {pilot.Item2} \n CharacterID: {pilot.Item3}");
                }*/
                await message.Channel.SendMessageAsync($"Total pilots found: {searchResult.Count}.");
                await message.Channel.SendMessageAsync($"Adding to database.");
                await message.Channel.SendMessageAsync($"Database OK.");
                await message.Channel.SendMessageAsync($"Preparing for emails.");
                await message.Channel.SendMessageAsync($"Email QUE OK.");
            }
            if (_client.GetChannel(_channelId) == message.Channel && message.Content.Contains("!character"))
            {
                String characterName = message.Content.Substring(11);
                String searchResult = new ZkillboardCharacterSearch().SearchForCharacter(characterName).GetAwaiter().GetResult();
                await message.Channel.SendMessageAsync(searchResult);
            }
            if (_client.GetChannel(_channelId) == message.Channel && message.Content.Contains("!test"))
            {
                await message.Channel.SendMessageAsync("I'm working!");
            }
            if (_client.GetChannel(_channelId) == message.Channel && message.Content.Equals("!exit"))
            {
                ExitApplication();
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        
        private void ExitApplication()
        {
            Environment.Exit(0);
        }
    }
}