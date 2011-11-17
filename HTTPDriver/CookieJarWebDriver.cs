using System.Collections.ObjectModel;
using System.Linq;
using HTTPDriver.Browser.Cookies;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class CookieJarWebDriver : CookieJar, ICookieJar
    {
        public void AddCookie(Cookie cookie)
        {
            base.AddCookie(new System.Net.Cookie(cookie.Name, cookie.Value));
        }

        public Cookie GetCookieNamed(string name)
        {
            var browserCookie = base.GetCookieNamed(name);
            return new Cookie(browserCookie.Name, browserCookie.Value);
        }

        public void DeleteCookie(Cookie cookie)
        {
            base.DeleteCookieNamed(cookie.Name);
        }

        public ReadOnlyCollection<Cookie> AllCookies
        {
            get { return new ReadOnlyCollection<Cookie>(base.AllCookies.Select(cook => new Cookie(cook.Name, cook.Value)).ToList()); }
        }
    }
}