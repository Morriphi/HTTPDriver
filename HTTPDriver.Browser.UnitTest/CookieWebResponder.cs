using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;

namespace HTTPDriver.Browser.UnitTest
{
    public class CookieWebResponder : IWebResponder
    {
        private WebHeaderCollection _cookies = new WebHeaderCollection();
        public CookieWebResponder(string cookiesToSet)
        {        
            Headers.Add(HttpResponseHeader.SetCookie, cookiesToSet);
        }
        public CookieWebResponder(IEnumerable<string> cookiesToSet)
        {
            foreach (var cookie in cookiesToSet)
            {                
                Headers.Add(HttpResponseHeader.SetCookie, cookie);
            }
        }
        public HtmlNode Page
        {
            get { return null; }
        }

        public HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.OK; }
        }

        public WebHeaderCollection Headers { get { return _cookies; } }
    }
}