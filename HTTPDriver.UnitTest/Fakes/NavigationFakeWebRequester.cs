using HTTPDriver.Browser;
namespace HTTPDriver.UnitTest.Fakes
{
    public class NavigationFakeWebRequester : IWebRequester
    {
        public IWebResponder Get(string url)
        {
            return new FakeWebResponder(new HtmlParser(""));
        }
    }
}