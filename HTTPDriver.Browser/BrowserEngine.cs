using System;
using System.Linq;
using System.Net;
using HTTPDriver.Browser.Cookies;

namespace HTTPDriver.Browser
{
    public class BrowserEngine
    {
        private readonly IWebRequester _requester;

        public CookieJar Cookies { get; private set; }

        public BrowserEngine(IWebRequester requester)
        {
            _requester = requester;
            Cookies = new CookieJar();
        }

        public void Load(string location)
        {
            var webResponder = _requester.Get(location);

            Location = new Uri(location);
            Page = new Page(webResponder.Page);
            ResponseStatusCode = webResponder.StatusCode;
            Headers = webResponder.Headers;
            PopulateCookies(webResponder);
            
        }

        private void PopulateCookies(IWebResponder webResponder)
        {
            bool containsSetCookieHeader = (webResponder.Headers[HttpResponseHeader.SetCookie] != null);
            if (containsSetCookieHeader)
            {
                var setCookieHeader = webResponder.Headers[HttpResponseHeader.SetCookie];
                Cookies.AddCookie(CookieParser.ParseCookie(setCookieHeader));
            }
        }

        public Uri Location { get; private set; }

        public Page Page { get; private set; }

        public HttpStatusCode ResponseStatusCode { get; private set; }

        public WebHeaderCollection Headers { get; private set; }
    }
}
