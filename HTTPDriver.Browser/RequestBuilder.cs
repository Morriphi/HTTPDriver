using System.Collections.Generic;
using System.Net;

namespace HTTPDriver.Browser
{
    public class RequestBuilder
    {
        private readonly HttpWebRequest _request;

        protected internal RequestBuilder(string url)
        {
            _request = (HttpWebRequest) WebRequest.Create(url);
            _request.CookieContainer = new CookieContainer();
        }

        public RequestBuilder WithAllowAutoRedirect(bool autoRedirect)
        {
            _request.AllowAutoRedirect = autoRedirect;
            return this;
        }

        public RequestBuilder WithCookies(IEnumerable<Cookie> cookies)
        {
            foreach (var cookie in cookies)
                Add(cookie);
            return this;
        }

        public RequestBuilder WithMethod(string method)
        {
            _request.Method = method;
            return this;
        }

        public RequestBuilder WithContentType(string contentType)
        {
            _request.ContentType = contentType;
            return this;
        }

        public RequestBuilder WithContent(string content)
        {
            _request.ContentLength = content.Length;
            return this;
        }

        private void Add(Cookie cookie)
        {
            cookie.Domain = _request.Address.Host;
            _request.CookieContainer.Add(cookie);
        }

        public WebResponder Submit()
        {
            return new WebResponder(_request.GetResponse());
        }
    }
}