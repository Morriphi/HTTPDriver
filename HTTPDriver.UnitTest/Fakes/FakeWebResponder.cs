using System;
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
        private readonly string _htmlParser;
        private readonly string _url;

        public FakeWebResponder(string htmlParser)
        {
            _htmlParser = htmlParser;
        }

        public FakeWebResponder(string htmlParser, string url)
        {
            _htmlParser = htmlParser;
            _url = url;
        }

        public string PageSource
        {
            get { return _htmlParser; }
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

        public WebHeaderCollection Headers
        {
            get { return null; }
        }

        public CookieCollection Cookies
        {
            get { return new CookieCollection(); }
        }

        public Uri Url
        {
            get { return new Uri(_url); }
        }

        public IEnumerable<Cookie> GetCookies()
        {
            throw new System.NotImplementedException();
        }
    }
}
