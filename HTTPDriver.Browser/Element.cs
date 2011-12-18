using System;
using HtmlAgilityPack;

namespace HTTPDriver.Browser
{
    public class Element
    {
        private readonly HtmlNode _htmlNode;
        public event Action<Element> Clicked;

        public Element(HtmlNode htmlNode)
        {
            _htmlNode = htmlNode;
        }

        public void Click()
        {
            Clicked(this);
        }

        public string Id
        {
            get { return _htmlNode.Id; }
        }

        public HtmlNode HtmlNode
        {
            get { return _htmlNode; }
        }
    }
}
