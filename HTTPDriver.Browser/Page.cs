using HtmlAgilityPack;

namespace HTTPDriver.Browser
{
    public class Page
    {
        private readonly HtmlNode _html;

        public Page(HtmlNode html)
        {
            _html = html;
        }

        public Page(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            _html = htmlDocument.DocumentNode.FirstChild;
        }

        public string Title()
        {
            var title = _html.SelectSingleNode("//title");
            return title != null ? title.Text() : "";
        }

        public string Html()
        {
            return _html.OuterHtml;
        }

        public string SelectSingleNodeText(string xpath)
        {
            return _html.SelectSingleNode(xpath).Text();
        }

        public HtmlNode HtmlNode()
        {
            return _html;
        }

        public Element FindById(string id)
        {
            return new Element(_html.OwnerDocument.GetElementbyId(id));
        }
    }
}