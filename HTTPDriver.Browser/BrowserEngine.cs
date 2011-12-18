using System;
using System.Net;

namespace HTTPDriver.Browser
{
    public class BrowserEngine
    {
        private readonly IWebRequester _requester;
        private readonly History _history;
        private IWebResponder _webResponder;
        private Page _currentPage;

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
            foreach (var cookie in Cookies)
                _requester.AddCookie(cookie);

            _webResponder = _requester.Get(location);
            _currentPage = new Page(_webResponder.Page);
            _currentPage.FormSubmitted += FormSubmitted;

            Location = new Uri(_webResponder.Url.ToString());
            PopulateCookies();
        }

        void FormSubmitted(FormSubmission formDetails)
        {
            Load(formDetails.Action);
        }

        private void PopulateCookies()
        {
            if (ResponseHasCookies())
                AddCookies();
        }

        private bool ResponseHasCookies()
        {
            return _webResponder.Cookies.Count > 0;
        }

        private void AddCookies()
        {
            foreach (Cookie cookie in _webResponder.Cookies)
                Cookies.AddCookie(cookie);
        }

        public Uri Location { get; private set; }

        public Page Page 
        {
            get { return _currentPage; }
        }

        public HttpStatusCode ResponseStatusCode
        {
            get { return _webResponder.StatusCode; }
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

        public void AddCookie(Cookie cookie)
        {
            Cookies.AddCookie(cookie);
        }
    }
}
