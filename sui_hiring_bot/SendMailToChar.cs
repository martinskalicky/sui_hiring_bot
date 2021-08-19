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
                httpClient_token = new HttpClient();
                httpClient_token.DefaultRequestHeaders.Accept.Clear();
                //App Client ID:Secret Key from https://developers.eveonline.com
                var byteArray = System.Text.Encoding.ASCII.GetBytes("Client ID:Secret Key");
                httpClient_token.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var values = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "grant_type", "refresh_token" },
                    { "refresh_token", "XXXXXXXXXXXXXXXX" } //REFRESH TOKEN OF SENDER
                };
                string url = "https://login.eveonline.com/v2/oauth/token";
                var data = new FormUrlEncodedContent(values);
                var res = await httpClient_token.PostAsync(url,data);
                string responseBody = await res.Content.ReadAsStringAsync();
                dynamic stuff = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody);
                
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                var characterIdSender = "XXXXXX"; //SENDER ID
                var token = stuff.access_token.ToString();

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
