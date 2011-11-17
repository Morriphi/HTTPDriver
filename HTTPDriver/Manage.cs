using HTTPDriver.Browser.Cookies;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class Manage : IOptions
    {
        private readonly ICookieJar _cookieJar;

        public Manage(CookieJar cookieJar)
        {
            _cookieJar = new CookieJarWebDriver(cookieJar);    
        }
        public ITimeouts Timeouts()
        {
            throw new System.NotImplementedException();
        }

        public ICookieJar Cookies
        {
            get { return _cookieJar; }
        }

        public IWindow Window
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}