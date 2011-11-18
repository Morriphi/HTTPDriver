using System.Collections.Generic;
using HtmlAgilityPack;

namespace HTTPDriver.Browser.UnitTest.Fakes
{
    public class FakeWebRequester : IWebRequester
    {
        private readonly FakeWebResponder _responder;

        public FakeWebRequester(FakeWebResponder responder)
        {
            _responder = responder;
        }

        public FakeWebRequester(HtmlNode document)
        {
            _responder = new FakeWebResponder(document);
        }

        public FakeWebRequester(string html)
        {
            _responder = new FakeWebResponder(new HtmlNodeBuilder(html).Build());
        }

        public IWebResponder Get(string url)
        {
            return _responder;
        }

        public void AddCookies(IEnumerable<string> cookiesToSet)
        {
            _responder.AddCookies(cookiesToSet);
        }

        public void AddCookie(string cookieToSet)
        {
            _responder.AddCookie(cookieToSet);
        }
    }
}
