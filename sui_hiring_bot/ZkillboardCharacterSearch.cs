using System;
using System.Threading.Tasks;
using System.Net.Http;
using Discord.WebSocket;
using System.Net.Http.Headers;

namespace sui_hiring_bot
{
    class ZkillboardCharacterSearch
    {
        private HttpClient httpClient;

        public async Task<String> SearchForCharacter(String characterName){
            try
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://zkillboard.com/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("autocomplete/" + characterName);
                if (response.IsSuccessStatusCode)
                {
                    Entity[] entities = await response.Content.ReadAsAsync<Entity[]>();
                    int numberFound = 0;
                    String resp = "";
                    String respDetail = "";

                    foreach (Entity entity in entities)
                    {
                        if(entity.type.Equals("character")){
                            numberFound++;
                            respDetail += "---------------------------\n";
                            respDetail += "Name: " + entity.name + "\n";
                            respDetail += "Id: " + entity.id + "\n";
                            respDetail += "---------------------------";
                        }
                    }
                    if(numberFound > 0){
                        resp = numberFound + " character(s) found with that name:\n" + respDetail;
                    }
                    else{
                        resp = "No characters found with that name.";
                    }

                    return resp;
                }
                else
                {
                    Console.WriteLine("error! Response:");
                    Console.WriteLine(response);
                    return "Error obtaining data from zkillboard for a character with the name '" + characterName + "'";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception obtaining data from zkillboard for a character with the name '" + characterName + "'");
                return "Error obtaining data from zkillboard for a character with the name '" + characterName + "'";

            }
        }
    }
}