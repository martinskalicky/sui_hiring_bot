using System;
using System.Threading.Tasks;
using System.Net.Http;
using Discord.WebSocket;
using System.Net.Http.Headers;

namespace sui_hiring_bot
{
    class SendMailToChar
    {
        private HttpClient httpClient;

        public async Task<bool> SendMailTo(String characterId)
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();

                //Mail from Character with ID:
                //TODO: Get this from database
                var characterIdSender = 123456;
                //ESI Token from Sender:
                //TODO: Get this from database
                var token = "";
                //Mail Data
                //TODO: Add templates for body
                var json = "{\"approved_cost\": 0,\"body\": \"This is the text body of a message sent by Discord Bot\",\"recipients\": [{\"recipient_id\": " + characterId + ",\"recipient_type\": \"character\"}],\"subject\": \"Its alive!!\"}";

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
