using System;
using System.Net;
using HTTPDriver.Browser.Cookies;

namespace HTTPDriver.Browser
{
    public class BrowserEngine
    {
        private readonly IWebRequester _requester;
        private readonly History _history;

        public CookieJar Cookies { get; private set; }

        public BrowserEngine(IWebRequester requester)
        {
            _requester = requester;
            _history = new History();
            Cookies = new CookieJar();
        }

        public void Load(string location)
        {
            GoTo(location);

            _history.Add(location);
        }

        private void GoTo(string location)
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
            if (webResponder.Headers != null)
            {
                bool containsSetCookieHeader = (webResponder.Headers[HttpResponseHeader.SetCookie] != null);
                if (containsSetCookieHeader)
                {
                    var setCookieHeader = webResponder.Headers[HttpResponseHeader.SetCookie];
                    Cookies.AddCookie(CookieParser.ParseCookie(setCookieHeader));
                }
            }
        }

        public Uri Location { get; private set; }

        public Page Page { get; private set; }

        public HttpStatusCode ResponseStatusCode { get; private set; }

        public WebHeaderCollection Headers { get; private set; }

        public void SetCurrent(string url)
        {
            _history.SetCurrent(url);
            GoTo(url);
        }

        public void Back()
        {
            _history.Back();
            GoTo(_history.CurrentUrl());
        }

        public void Forward()
        {
            _history.Forward();
            GoTo(_history.CurrentUrl());
        }
    }
}
