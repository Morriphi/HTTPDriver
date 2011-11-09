namespace HTTPDriver.UnitTest.Fakes
{
    public class NavigationFakeWebRequester : IWebRequester
    {
        public IWebResponder Request(string url)
        {
            return new FakeWebResponder(new HtmlParser(""));
        }
    }
}