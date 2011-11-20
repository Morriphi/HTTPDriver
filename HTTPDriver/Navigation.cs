using System;
using HTTPDriver.Browser;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class Navigation : INavigation
    {
        private readonly HttpDriver _driver;
        private readonly BrowserEngine _browser;

        public Navigation(HttpDriver driver)
        {
            _driver = driver;
            _browser = driver.GetBrowser();
        }

        public void Back()
        {
            _browser.Back();
        }

        public void Forward()
        {
            _browser.Forward();
        }

        public void GoToUrl(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                if (Uri.IsWellFormedUriString(uri, UriKind.Relative))
                {
                    uri = _browser.Location.ToString().FromRelativeUrl(uri);
                }
                else
                {
                    throw new UriFormatException();
                }
            }

            _driver.SendRequest(uri);
        }

        public void GoToUrl(Uri url)
        {
           GoToUrl(url.AbsoluteUri);
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
