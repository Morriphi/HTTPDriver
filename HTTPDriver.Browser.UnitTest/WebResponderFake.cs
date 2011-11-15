using System;
using HtmlAgilityPack;

namespace HTTPDriver.Browser.UnitTest
{
    public class WebResponderFake : IWebResponder
    {
        private readonly HtmlNode _document;

        public WebResponderFake(HtmlNode document)
        {
            _document = document;
        }

        public HtmlNode Page
        {
            get { return _document; }
        }
    }
}
