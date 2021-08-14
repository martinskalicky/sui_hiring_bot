using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Net.Http;
using System.Net.Http.Headers;

namespace sui_hiring_bot
{
    class Program
    {
        private DiscordSocketClient _client;
        private ulong _channelId;
        private string? _token;
        private HttpClient httpClient;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _channelId = Convert.ToUInt64(Environment.GetEnvironmentVariable("CHANNEL_ID"));
            _client.MessageReceived += MessageReceived;

            _token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
            Console.WriteLine("_token = " + _token);

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://zkillboard.com/autocomplete/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            await Task.Delay(-1);
        }
        private async Task MessageReceived(SocketMessage message)
        {
            if (_client.GetChannel(_channelId) == message.Channel && message.Content.Contains("!character"))
            {
                String characterName = message.Content.Substring(11);
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(characterName);
                    if (response.IsSuccessStatusCode)
                    {
                        String resp = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(resp);
                        await message.Channel.SendMessageAsync(resp);
                    }
                    else
                    {
                        Console.WriteLine("error! Response:");
                        Console.WriteLine(response);
                        await message.Channel.SendMessageAsync("Error obtaining data from zkillboard for a character with the name '" + characterName + "'");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
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