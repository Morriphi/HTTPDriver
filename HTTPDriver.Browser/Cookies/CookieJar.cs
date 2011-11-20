using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace HTTPDriver.Browser.Cookies
{
    public class CookieJar
    {
        private readonly Dictionary<string, Cookie> _cookies;

        public CookieJar()
        {
            _cookies = new Dictionary<string, Cookie>();
        }

        public void AddCookie(Cookie cookie)
        {
            _cookies.Add(cookie.Name, cookie);
        }

        public Cookie GetCookieNamed(string name)
        {
            return _cookies[name];
        }

        public void DeleteCookie(Cookie cookie)
        {
            DeleteCookieNamed(cookie.Name);
        }

        public void DeleteCookieNamed(string name)
        {
            _cookies.Remove(name);
        }

        public void DeleteAllCookies()
        {
            _cookies.Clear();
        }

        public ReadOnlyCollection<Cookie> AllCookies
        {
            get { return new ReadOnlyCollection<Cookie>(_cookies.Values.ToList()); }
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return _cookies.Values.GetEnumerator();
        }
    }
}