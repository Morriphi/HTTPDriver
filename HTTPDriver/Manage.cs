using System.Collections.ObjectModel;
using System.Linq;
using HTTPDriver.Browser;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class Manage : IOptions, ICookieJar
    {
        private readonly CookieJar _jar;

        public Manage(CookieJar jar)
        {
            _jar = jar;
        }

        public ITimeouts Timeouts()
        {
            throw new System.NotImplementedException();
        }

        public ICookieJar Cookies
        {
            get { return this; }
        }

        public IWindow Window
        {
            get { throw new System.NotImplementedException(); }
        }

        public void AddCookie(Cookie cookie)
        {
            _jar.AddCookie(new System.Net.Cookie(cookie.Name, cookie.Value));
        }

        public Cookie GetCookieNamed(string name)
        {
            var browserCookie = _jar.GetCookieNamed(name);
            return new Cookie(browserCookie.Name, browserCookie.Value);
        }

        public void DeleteCookie(Cookie cookie)
        {
            _jar.DeleteCookieNamed(cookie.Name);
        }

        public void DeleteCookieNamed(string name)
        {
            _jar.DeleteCookieNamed(name);
        }

        public void DeleteAllCookies()
        {
            _jar.DeleteAllCookies();
        }

        public ReadOnlyCollection<Cookie> AllCookies
        {
            get
            {
                return new ReadOnlyCollection<Cookie>(
                    _jar.AllCookies.Select(cook => new Cookie(cook.Name, cook.Value)).ToList());
            }
        }
    }
}