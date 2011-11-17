namespace HTTPDriver.Browser.UnitTest
{
    public class CookieWebRequester : IWebRequester
    {
        private IWebResponder _responder;
        public CookieWebRequester(IWebResponder cookieWebResponder)
        {
            _responder = cookieWebResponder;
        }

        public IWebResponder Get(string url)
        {
            return _responder;
        }
    }
}