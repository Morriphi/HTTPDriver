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
    }
}