using System;
using System.Linq;
using System.Net;

namespace HTTPDriver.Browser.Cookies
{
    public static class CookieParser
    {
        public static Cookie ParseCookie(string value)
        {
            var cookieParts = value.Split(';');
            var valuePart = cookieParts[0];
            var idx = valuePart.IndexOf("=");
            var cookieName = valuePart.Substring(0, idx);
            var cookieValue = valuePart.Substring(idx + 1, valuePart.Length - idx - 1);

            string expiresDateString = null;
            foreach (var cookiePart in cookieParts.Skip(1))
                if (cookiePart.Trim().StartsWith("expires="))
                    expiresDateString = cookiePart.Split('=')[1];

            if (!string.IsNullOrEmpty(expiresDateString))
            {
                return new Cookie(cookieName, cookieValue, "", "")
                           {
                               Expires = DateTime.Parse(expiresDateString)
                           };
            }

            return new Cookie(cookieName, cookieValue);
        }
    }
}