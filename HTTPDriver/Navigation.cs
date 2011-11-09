using System;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class Navigation : INavigation
    {
        private readonly HttpDriver _driver;
        private readonly History _history;

        public Navigation(HttpDriver driver)
        {
            _history = new History();
            _driver = driver;
        }

        public void Back()
        {
            _history.Back();
            _driver.Url = _history.CurrentUrl();
        }

        public void Forward()
        {
           _history.Forward();
           _driver.Url = _history.CurrentUrl();
        }

        public void GoToUrl(string url)
        {
            if(NotWellFormed(url))
                throw new UriFormatException();

            _history.Add(url);
            _driver.Url = _history.CurrentUrl();
            _driver.SendRequest();
        }

        public void GoToUrl(Uri url)
        {
            _driver.Url = url.AbsoluteUri;
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        private static bool NotWellFormed(string url)
        {
            return !Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
