using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace sui_hiring_bot
{
    static class ZkillboardScanShips
    {
        public static List<Tuple<string, string, string>> ScanForShips(){
            var html = @"https://zkillboard.com/ship/22544/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
            Console.WriteLine("Title: " + node.InnerText);
            var node2 = htmlDoc.DocumentNode.DescendantsAndSelf("");
            List<Tuple<string, string, string>> returnPilots = new List<Tuple<string, string, string>>();
            
            var pilots = htmlDoc.DocumentNode.SelectNodes("/html[1]/body[1]/div[1]/div[2]/span[1]/div[3]/div[1]/table[1]/tbody[1]/tr");
            foreach (var select in pilots)
            {
                var pilotName = "";
                var pilotSystem = "";
                var pilotCharacterID = "";
                if (select.OuterHtml.Contains("victim"))
                {
                    var parseTd = select.Descendants("td");
                    foreach (var td in parseTd)
                    {
                        if (td.OuterHtml.Contains("character"))
                        {
                            if (td.InnerText.Split("\n")[1].Contains("Hulk"))
                            {
                                pilotName = td.InnerText.Split("\n")[1].Replace("(Hulk)","");
                                pilotCharacterID = td.OuterHtml.Split("/")[2];
                            }
                        }
                        if (td.OuterHtml.Contains("system"))
                        {
                            pilotSystem = td.InnerText.Split("\n")[1];
                        }
                    }
                    returnPilots.Add(new Tuple<string, string, string>(pilotName, pilotSystem, pilotCharacterID));
                }
            }
            return returnPilots;
        }
    }
}