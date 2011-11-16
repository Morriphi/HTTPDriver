using System.Collections.Generic;
using System.Net;
using HTTPDriver.Browser;
using HTTPDriver.Browser.UnitTest;
using HtmlAgilityPack;
using Cookie = OpenQA.Selenium.Cookie;

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

        public HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.OK; }
        }

        public IEnumerable<Cookie> GetCookies()
        {
            throw new System.NotImplementedException();
        }
    }
}
