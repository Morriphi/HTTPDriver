using System;
using System.Net;

namespace HTTPDriver.Browser
{
    public class BrowserEngine
    {
        private readonly IWebRequester _requester;

        public BrowserEngine(IWebRequester requester)
        {
            _requester = requester;
        }

        public void Load(string location)
        {
            var webResponder = _requester.Get(location);

            Location = new Uri(location);
            Page = new Page(webResponder.Page);
            ResponseStatusCode = webResponder.StatusCode;
            Headers = webResponder.Headers;
        }

        public Uri Location { get; private set; }

        public Page Page { get; private set; }

        public HttpStatusCode ResponseStatusCode { get; private set; }

        public WebHeaderCollection Headers { get; private set; }
    }
}
