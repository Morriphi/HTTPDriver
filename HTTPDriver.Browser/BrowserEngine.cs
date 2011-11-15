using System;
using HtmlAgilityPack;

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
            Location = new Uri(location);
            Page = _requester.Get(location).Page;
        }

        public Uri Location { get; private set; }

        public HtmlNode Page { get; private set; }
    }
}
