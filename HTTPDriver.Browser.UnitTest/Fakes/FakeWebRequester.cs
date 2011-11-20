using System.Collections.Generic;
using System.Net;

namespace HTTPDriver.Browser.UnitTest.Fakes
{
    public class FakeWebRequester : IWebRequester
    {
        private readonly string _html;
        private FakeWebResponder _responder;
        private readonly IList<Cookie> _cookies;

        public FakeWebRequester(string html)
        {
            _html = html;
            _cookies = new List<Cookie>();
        }

        public IWebResponder Get(string url)
        {
            _responder = new FakeWebResponder(new HtmlNodeBuilder(_html).Build(), url);

            foreach (var cookie in _cookies)
                _responder.AddCookie(cookie);

            return _responder;
        }

        public void AddCookie(Cookie cookieToSet)
        {
            _cookies.Add(cookieToSet);
        }
    }
}
