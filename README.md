# HtmlAgilityPack？

HtmlAgilityPack 是 .NET 下的一个 HTML 解析类库。支持用 XPath 来解析 HTML 。命名空间： HtmlAgilityPack

# 核心程序代码

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