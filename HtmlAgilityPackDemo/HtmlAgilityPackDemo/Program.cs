using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;

namespace HtmlAgilityPackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                var url = "https://meiriyiwen.com/random/";

                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"article_show\"]");
                node.Descendants()
                    .Where(n => n.Name == "script" || n.Name == "style" || n.Name == "#comment" || n.Id == "bdshare")
                    .ToList().ForEach(n => n.Remove());
                var title = node.SelectSingleNode("//h1").InnerText;
                var author = node.SelectSingleNode("//*[@class=\"article_author\"]").InnerText;
                var article = node.SelectSingleNode("//*[@class=\"article_text\"]").InnerText.TrimStart();

                Console.WriteLine(title + "\r\n");
                Console.WriteLine(author + "\r\n");
                Console.WriteLine(article);

                StreamWriter stream = new StreamWriter(@"F:\Article\" + title + "-" + author + ".txt");

                stream.WriteLine(title + "\r\n");
                stream.WriteLine(author + "\r\n");
                stream.WriteLine(article);

                stream.Close();
                stream.Dispose();
            }
        }
    }
}
