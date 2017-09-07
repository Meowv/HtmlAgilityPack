using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Novel
{
    class Program
    {
        static void Main(string[] args)
        {
            var domain = "http://www.jueshitangmen.info";

            HtmlWeb web = new HtmlWeb();

            HtmlDocument doc = web.Load(domain + "/zhetian/");
            List<HtmlNode> nodeList = doc.DocumentNode.SelectNodes("//*[@class=\"panel\"]/ul/li").AsParallel().ToList();

            foreach (var item in nodeList)
            {
                HtmlNode html = item.SelectSingleNode(".//span/a");
                var title = html.InnerText;

                var url = html.Attributes["href"].Value;
                HtmlDocument document = web.Load(url);

                HtmlNode node = document.DocumentNode.SelectSingleNode("//*[@class=\"content\"]");
                node.Descendants()
                    .Where(n => n.Name == "script" || n.Name == "style" || n.Name == "#comment")
                    .ToList().ForEach(n => n.Remove());

                var content = node.InnerText.Trim();

                Console.WriteLine(title + "\r\n");
                Console.WriteLine(content);

                StreamWriter stream = new StreamWriter(@"F:\Article\遮天\" + title + ".txt");

                stream.WriteLine(title + "\r\n");
                stream.WriteLine(content);

                stream.Close();
                stream.Dispose();
            }

        }
    }
}
