using HtmlAgilityPack;

namespace HTTPDriver.Browser.UnitTest
{
    public class WebRequesterFake : IWebRequester
    {
        private readonly IWebResponder _responder;

        public WebRequesterFake(IWebResponder responder)
        {
            _responder = responder;
        }

        public WebRequesterFake(HtmlNode document)
        {
            _responder = new WebResponderFake(document);
        }

        public WebRequesterFake(string html)
        {
            _responder = new WebResponderFake(new HtmlNodeBuilder(html).Build());
        }

        public IWebResponder Get(string url)
        {
            return _responder;
        }
    }
}
