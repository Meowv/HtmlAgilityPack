using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Spider
{
    class Program
    {
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

        public static bool DownPic(string PicSourceUrl, string filePath)
        {
            WebRequest request = WebRequest.Create(PicSourceUrl);
            WebResponse response = request.GetResponse();
            Stream reader = response.GetResponseStream();
            FileStream writer = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            try
            {
                byte[] buff = new byte[512];
                int c = 0; //实际读取的字节数  
                while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                {
                    writer.Write(buff, 0, c);
                }
                writer.Close();
                writer.Dispose();
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
            return true;
        }
    }
}
