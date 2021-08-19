using System;
using System.Threading.Tasks;
using System.Net.Http;
using Discord.WebSocket;
using System.Net.Http.Headers;

namespace sui_hiring_bot
{
    class SendMailToChar
    {
        public static HttpClient httpClient;

        public static async Task<bool> SendMailTo(int characterId, string emailPayload, string token, int characterIdSender)
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();

                var json = "{\"approved_cost\": 0,\"body\": \"" + emailPayload + "\",\"recipients\": [{\"recipient_id\": " + characterId + ",\"recipient_type\": \"character\"}],\"subject\": \"Its alive!!\"}";

                var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.PostAsync("https://esi.evetech.net/latest/characters/" + characterIdSender + "/mail/?datasource=tranquility", data);
                string result = response.Content.ReadAsStringAsync().Result;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception sending data to Eve Online Character '" + characterId + "'");
                return false;

            }
        }
    }
}
