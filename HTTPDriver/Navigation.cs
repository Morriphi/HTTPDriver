using System;
using HTTPDriver.Browser;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class Navigation : INavigation
    {
        private readonly HttpDriver _driver;
        private readonly BrowserEngine _engine;

        public Navigation(HttpDriver driver)
        {
            _driver = driver;
            _engine = driver.GetBrowser();
        }

        public void Back()
        {
            _engine.Back();
            _driver.Url = _engine.Location.ToString();
        }

        public void Forward()
        {
            _engine.Forward();
            _driver.Url = _engine.Location.ToString();
        }

        public void GoToUrl(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                if (Uri.IsWellFormedUriString(uri, UriKind.Relative))
                {
                    uri = _engine.Location.ToString().FromRelativeUrl(uri);
                }
                else
                {
                    throw new UriFormatException();
                }
            }

            _driver.Url = uri;
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
