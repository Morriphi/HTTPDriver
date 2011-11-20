using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace HTTPDriver.Browser.UnitTest.Fakes
{
    public class FakeWebResponder : IWebResponder
    {
        private readonly HtmlNode _document;
        private readonly WebHeaderCollection _header;
        private readonly CookieCollection _cookies;

        public FakeWebResponder(HtmlNode document)
        {
            _document = document;
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

        public void AddCookie(Cookie cookie)
        {
            _cookies.Add(cookie);
        }
    }
}
