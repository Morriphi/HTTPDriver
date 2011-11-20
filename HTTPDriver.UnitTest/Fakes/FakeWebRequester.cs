using System.Collections.Generic;
using System.Net;
using HTTPDriver.Browser;

namespace HTTPDriver.UnitTest.Fakes
{
    public class FakeWebRequester : IWebRequester
    {
        private readonly Dictionary<string, string> _responses = new Dictionary<string, string>();

        public void AddTestResponseString(string url, string pageContent)
        {
            _responses.Add(url, pageContent);
        }

        public IWebResponder Get(string url)
        {
            return new FakeWebResponder(_responses[url], url);
        }

        public void AddCookie(Cookie cookie)
        {
            
        }
    }
}
