### HtmlAgilityPack使用学习，爬取文章、美女图片、小说...

# HtmlAgilityPack？

HtmlAgilityPack 是 .NET 下的一个 HTML 解析类库。支持用 XPath 来解析 HTML 。命名空间： HtmlAgilityPack

# 爬取美女图片核心代码

```
    static void Main(string[] args)
    {
        HtmlWeb web = new HtmlWeb();
        string path = @"F:\pic\";
        
        for (int i = 88; i >= 1; i--)
        {
            var url = "http://jandan.net/ooxx/page-" + i;

            HtmlDocument doc = web.Load(url);

            List<HtmlNode> nodeList = doc.DocumentNode.SelectNodes("//*[@class=\"commentlist\"]/li").AsParallel().ToList();

            foreach (var item in nodeList)
            {
                HtmlNode imghtml = item.SelectSingleNode(".//img");
                var imgsrc = "http:" + imghtml.Attributes["src"].Value;
                var imgname = Guid.NewGuid().ToString() + imgsrc.Substring(imgsrc.Length - 4, 4);

                Console.WriteLine(imgsrc);

                DownPic(imgsrc, path + imgname);
            }
        }
    }
```

# 爬取每日一文核心代码

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

# 爬取小说核心代码

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