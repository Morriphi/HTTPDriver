using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace HTTPDriver.Browser.UnitTest.Fakes
{
    public class FakeWebResponder : IWebResponder
    {
        private readonly HtmlNode _document;
        private readonly WebHeaderCollection _header;

        public FakeWebResponder(HtmlNode document)
        {
            _document = document;
            _header = new WebHeaderCollection();
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

        public void AddCookies(IEnumerable<string> cookiesToSet)
        {
            foreach (var cookie in cookiesToSet)
                AddCookie(cookie);
        }

        public void AddCookie(string cookieToSet)
        {
            Headers.Add(HttpResponseHeader.SetCookie, cookieToSet);
        }
    }
}
