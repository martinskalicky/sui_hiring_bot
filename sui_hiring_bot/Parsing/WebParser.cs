using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace sui_hiring_bot
{
    public class WebParser
    {
        public static List<Tuple<string, string, string>> ScanForShips(string shipId, string shipName){
            var baseUrl = $"https://zkillboard.com/ship/{shipId}/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(baseUrl);
            List<Tuple<string, string, string>> returnPilots = new List<Tuple<string, string, string>>();
            
            var pilots = htmlDoc.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[2]/span[1]/div[3]/div[1]/table[1]/tbody[1]/tr");
            foreach (var select in pilots)
            {
                var pilotName = "";
                var pilotSystem = "";
                var pilotCharacterId = "";
                if (select.OuterHtml.Contains("victim"))
                {
                    var parseTd = select.Descendants("td");
                    foreach (var td in parseTd)
                    {
                        if (td.OuterHtml.Contains("character"))
                        {
                            if (td.InnerText.Split("\n")[1].Contains(shipName))
                            {
                                pilotName = td.InnerText.Split("\n")[1].Replace($"({shipName})","");
                                pilotCharacterId = td.OuterHtml.Split("/")[2];
                            }
                        }
                        if (td.OuterHtml.Contains("system"))
                        {
                            pilotSystem = td.InnerText.Split("\n")[1];
                            pilotSystem = pilotSystem.Split(" ")[0];
                        }
                    }
                    returnPilots.Add(new Tuple<string, string, string>(pilotName, pilotSystem, pilotCharacterId));
                }
            }
            return returnPilots;
        }
    }
}