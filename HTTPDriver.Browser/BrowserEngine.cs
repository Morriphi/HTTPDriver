using System;
using System.Collections.Generic;
using System.Net;
using HTTPDriver.Browser.Cookies;

namespace HTTPDriver.Browser
{
    public class BrowserEngine
    {
        private readonly IWebRequester _requester;
        private readonly History _history;
        private IWebResponder _webResponder;

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
            _webResponder = _requester.Get(location);
            Location = new Uri(location);
            PopulateCookies();
        }

        private void PopulateCookies()
        {
            if (ResponseHasCookie())
                AddCookie();
        }

        private bool ResponseHasCookie()
        {
            return _webResponder.Cookies.Count > 0;
        }

        private void AddCookie()
        {
            foreach (var cookie in _webResponder.Cookies)
                Cookies.AddCookie(CookieParser.ParseCookie(cookie.ToString()));
        }

        public Uri Location { get; private set; }

        public Page Page 
        {
            get { return new Page(_webResponder.Page); }
        }

        public HttpStatusCode ResponseStatusCode
        {
            get { return _webResponder.StatusCode; }
        }

        public WebHeaderCollection Headers 
        {
            get { return _webResponder.Headers; }
        }

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
