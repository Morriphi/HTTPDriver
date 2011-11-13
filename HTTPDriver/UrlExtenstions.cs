using System;
using System.Linq;

namespace HTTPDriver
{
    public static class UrlExtenstions
    {
        /// <param name="currentUrl">Current absolute url</param>
        /// <param name="relative">Relative destination url</param>
        /// <returns></returns>
        public static string FromRelativeUrl(this string currentUrl, string relative)
        {
            if (!Uri.IsWellFormedUriString(currentUrl, UriKind.Absolute))
                throw new UriFormatException();

            var current = new Uri(currentUrl);

            if (current.PathAndQuery == "/")
                return new Uri(current, relative).ToString();

            var finalSegment = current.OriginalString.Substring(current.OriginalString.LastIndexOf('/'));

            if (finalSegment.Contains('?') || finalSegment.Contains('.'))
            {
                return current.OriginalString.Replace(finalSegment, "/" + relative);
            }

            if (!current.OriginalString.EndsWith("/"))
                current = new Uri(currentUrl + "/");

            return new Uri(current, relative).ToString();
        }
    }
}