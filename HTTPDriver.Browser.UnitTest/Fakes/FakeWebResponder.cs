using System;
using System.Net;
using HtmlAgilityPack;

namespace HTTPDriver.Browser.UnitTest.Fakes
{
    public class FakeWebResponder : IWebResponder
    {
        private readonly HtmlNode _document;
        private readonly string _url;
        private readonly WebHeaderCollection _header;
        private readonly CookieCollection _cookies;

        public FakeWebResponder(HtmlNode document, string url)
        {
            _document = document;
            _url = url;
            _header = new WebHeaderCollection();
            _cookies = new CookieCollection();

        }

        public HtmlNode Page
        {
            get { return _document; }
        }

        public HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.OK; }
        }

        public WebHeaderCollection Headers
        {
            get { return _header; }
        }

        public CookieCollection Cookies
        {
            get { return _cookies; }
        }

        public Uri Url
        {
            get { return new Uri(_url); }
        }

        public void AddCookie(Cookie cookie)
        {
            _cookies.Add(cookie);
        }
    }
}
