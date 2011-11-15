using System.Collections.Generic;
using HTTPDriver.Browser;
using HTTPDriver.Browser.UnitTest;
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

        public string PageSource
        {
            get { return _htmlParser.GetSourceCode(); }
        }

        public HtmlNode Page
        {
            get
            {
                var htmlBuilder = new HtmlNodeBuilder(PageSource);
                return htmlBuilder.Build();
            }
        }

        public IEnumerable<Cookie> GetCookies()
        {
            throw new System.NotImplementedException();
        }
    }
}
