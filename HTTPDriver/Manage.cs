using HTTPDriver.Browser.Cookies;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class Manage : IOptions
    {
        private ICookieJar _cookieJar;

        public Manage(CookieJar cookieJar)
        {
            _cookieJar = new CookieJarWebDriver();
            foreach (var cookie in cookieJar)
            {
                _cookieJar.AddCookie(new Cookie(cookie.Name, cookie.Value));
            }
            
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