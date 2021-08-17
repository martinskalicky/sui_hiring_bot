using System;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace sui_hiring_bot
{
    class ZkillboardScanShips
    {
        public async Task<String> ScanForShips(String[] ships){
            var html = @"https://zkillboard.com/ship/22544/";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");
            Console.WriteLine("Title: " + node.InnerText);

            return "Title: " + node.InnerText;
        }
    }
}