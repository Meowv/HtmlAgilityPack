# HtmlAgilityPack？

HtmlAgilityPack 是 .NET 下的一个 HTML 解析类库。支持用 XPath 来解析 HTML 。命名空间： HtmlAgilityPack

# HtmlAgilityPackDemo核心程序代码

```
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
```

# Novel核心程序代码

```
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
```