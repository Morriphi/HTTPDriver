using System.Collections.Generic;
using HtmlAgilityPack;
using OpenQA.Selenium;

namespace HTTPDriver.UnitTest.Fakes
{
    public class FakeWebResponder : IWebResponder
    {
        private readonly HtmlParser _htmlParser;

        public FakeWebResponder(HtmlParser htmlParser)
        {
            _htmlParser = htmlParser;
        }

        public string GetTitle()
        {
            return _htmlParser.GetTitle();
        }

        public string GetPageSource()
        {
            return _htmlParser.GetSourceCode();
        }

        public HtmlNode GetDocumentElement()
        {
            var htmlBuilder = new HtmlNodeBuilder(GetPageSource());
            return htmlBuilder.Build();
        }

        public IEnumerable<Cookie> GetCookies()
        {
            throw new System.NotImplementedException();
        }
    }
}
