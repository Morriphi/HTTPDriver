using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HTTPDriver.Browser
{
    public class WebRequester : IWebRequester
    {
        private bool _shouldFollowRedirects=true;
        private readonly IList<Cookie> _cookies = new List<Cookie>();

        public IWebResponder Get(string url)
        {
            return Request(url)
                .WithAllowAutoRedirect(_shouldFollowRedirects)
                .WithCookies(_cookies)
                .Submit();
        }

        public IWebResponder Post(string url, IDictionary<string, string>  postdata)
        {
            return Request(url)
                .WithMethod("POST")
                .WithContentType("application/x-www-form-urlencoded")
                .WithContent(CreatePostDataString(postdata))
                .Submit();
        }

        public WebRequester AutomaticallyFollowRedirects(bool shouldFollowRedirects)
        {
            _shouldFollowRedirects = shouldFollowRedirects;
            return this;
        }

        public void AddCookie(Cookie cookie)
        {
            _cookies.Add(cookie);
        }

        private RequestBuilder Request(string url)
        {
            return new RequestBuilder(url);
        }

        private string CreatePostDataString(IEnumerable<KeyValuePair<string, string>> postdata)
        {
            return string.Join("&", (from value in postdata
                    select value.Key + "=" + value.Value).ToArray());
        }
    }
}