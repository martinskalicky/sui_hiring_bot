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

        public async Task<String> SendMailTo(String characterId)
        {
            try
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();

                //Mail from Character with ID:
                var characterIdSender = 123456;
                //ESI Token from Sender:
                var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkpXVC1TaWduYXRasd1cmUtS2V5IiwidHlwIjoiSldUIn0.eyJzY3AiOlsiZXNpLW1haWwub3J1nYW5pemVfbWFpbC52MSIsImVzaS1tYWlsLnJlYWRfbWFpbC52MSIsImVzaS1tYWlsLnNlbmRfbWFpbC52MSJdLCJqdGkiOiI0ODA0MGNhMi0yOGY0LTRkMDUtYmIwNy1mZGJjN2Q4Y2Y0YzciLCJraWQiOiJKV1QtU2lnbmF0dXJlLUtleSIsInN1YiI6IkNIQVJBQ1RFUjpFVkU6MjExNzg0MTk3MSIsImF6cCI6IjY4MzA4NGFiNWY4ODQ4ZDRiMTg3NDYyYWMzYjk3Njc3IiwidGVuYW50IjoidHJhbnF1aWxpdHkiLCJ0aWVyIjoibGl2ZSIsInJlZ2lvbiI6IndvcmxkIiwibmFtZSI6IkQ0bmkzbCIsIm93bmVyIjoiY1UrTk5SVG9LMkJqak1GS1lJMEZ2anRjckxzPSIsImV4cCI6MTYyOTIwNjE3OSwiaXNzIjoibG9naW4uZXZlb25saW5lLmNvbSJ9.MmIm8LmbuGpzg1XWmq7D1OrtxePSOAMGZ1hoSB9yPaF1xFCbUz9jRSF_DsJ_dhvMRbBCn0AjUAPYSVAXRcGtKtiPoBXz62NiAAJ88tTQ6YBvcBwqxHGZ778VsQ42HvDLh98am7yl9rGl_xVNR-0j0wTA_4vatCqNat0ae_NVuhXt_R0tpHio5pQpm2A5hVEnO55XdYI9JoFMdE89U4Kpl0OQfqGVJnTiWqfxH0aS4Uz6jyGcJVi-vYADOoIBM6uNWsO0M4AJLOMdUz0Nsp7MmUZCN91cLKQJ0ru0bZrDeTjxkM92GoHTFW09fHD1A6u0uV1h-hjbveNLlplfarH5fA";
                //Mail Data
                var json = "{\"approved_cost\": 0,\"body\": \"This is the text body of a message sent by Discord Bot\",\"recipients\": [{\"recipient_id\": " + characterId + ",\"recipient_type\": \"character\"}],\"subject\": \"Its alive!!\"}";

                var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await httpClient.PostAsync("https://esi.evetech.net/latest/characters/" + characterIdSender + "/mail/?datasource=tranquility", data);
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(result);
                return response.StatusCode.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception sending data to Eve Online Character '" + characterId + "'");
                return "Exception sending data to Eve Online Character '" + characterId + "'";

            }
        }
    }
}
