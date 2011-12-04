using HtmlAgilityPack;

namespace HTTPDriver.Browser
{
    public class Element
    {
        private HtmlNode _htmlNode;

        public Element(HtmlNode htmlNode)
        {
            _htmlNode = htmlNode;
        }

        public void Click()
        {
            // TODO: validation here
        }
    }
}
