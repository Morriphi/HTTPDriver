using System.Collections.Generic;

namespace HTTPDriver.UnitTest.Fakes
{
    public class HttpDriverFakeWebRequester : IWebRequester
    {
        private readonly Dictionary<string, HtmlParser> _responses = new Dictionary<string, HtmlParser>();

        public void AddTestResponseString(string url, HtmlParser pageContent)
        {
            _responses.Add(url, pageContent);
        }

        public IWebResponder Request(string url)
        {
            return new FakeWebResponder(_responses[url]);
        }
    }
}
