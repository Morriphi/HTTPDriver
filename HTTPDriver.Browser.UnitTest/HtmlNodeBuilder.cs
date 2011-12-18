using HtmlAgilityPack;

namespace HTTPDriver.Browser.UnitTest
{
    public class HtmlNodeBuilder
    {
        private readonly string _markup;

        public HtmlNodeBuilder(string markup)
        {
            _markup = markup;
        }

        public HtmlNode Build()
        {
            HtmlNode.ElementsFlags.Remove("form");
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(_markup);
            return htmlDocument.DocumentNode.FirstChild;
        }
    }
}
