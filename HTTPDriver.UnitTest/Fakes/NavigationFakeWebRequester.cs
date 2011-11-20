using System.Net;
using HTTPDriver.Browser;
namespace HTTPDriver.UnitTest.Fakes
{
    public class NavigationFakeWebRequester : IWebRequester
    {
        public IWebResponder Get(string url)
        {
            return new FakeWebResponder("");
        }

        public void AddCookie(Cookie cookie)
        {
            
        }
    }
}