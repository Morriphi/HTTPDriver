using System.Collections.Generic;
using HTTPDriver.Browser;

namespace HTTPDriver.UnitTest.Fakes
{
    public class HttpDriverFakeWebRequester : IWebRequester
    {
        private readonly Dictionary<string, HtmlParser> _responses = new Dictionary<string, HtmlParser>();

        public void AddTestResponseString(string url, HtmlParser pageContent)
        {
            _responses.Add(url, pageContent);
        }

        public void AddTestResponseString(string url, string pageContent)
        {
            _responses.Add(url, new HtmlParser(pageContent));
        }

        public IWebResponder Get(string url)
        {
            return new FakeWebResponder(_responses[url]);
        }
    }
}
