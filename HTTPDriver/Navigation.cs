using System;
using HTTPDriver.Browser;
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

        public void GoToUrl(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                if (Uri.IsWellFormedUriString(uri, UriKind.Relative))
                {
                    uri = _history.CurrentUrl().FromRelativeUrl(uri);
                }
                else
                {
                    throw new UriFormatException();
                }
            }

            _history.Add(uri);
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

        
    }
}
