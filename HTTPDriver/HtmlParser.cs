using HtmlAgilityPack;

namespace HTTPDriver
{
    public class HtmlParser
    {
        private readonly HtmlDocument _htmlDocument;
        private readonly HtmlNode _documentRoot;

        public HtmlParser(string html)
        {
            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(html);
            _documentRoot = _htmlDocument.DocumentNode;
        }

        public string GetTitle()
        {
            var node = _documentRoot.SelectSingleNode("//title");

            return node != null ? node.InnerText : string.Empty;
        }
    }
}