using System.Net;
using HTTPDriver.Browser.Cookies;
using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest
{
    [TestFixture]
    public class BrowserTest
    {
        [Test]
        public void LoadsUrl()
        {
            const string location = "http://www.totaljobs.com/";
            var requester = new WebRequesterFake("<p>Hello world</p>");

            var browser = new BrowserEngine(requester);
            browser.Load(location);

            Assert.That(browser.Location.ToString(), Is.EqualTo(location));
            Assert.That(browser.Page, Is.InstanceOf<Page>());
            Assert.That(browser.Page.SelectSingleNodeText("//p"), Is.EqualTo("Hello world"));
            Assert.That(browser.ResponseStatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ShouldSetCookie()
        {
            string cookieText = "Chocolate=Chip; expires=Tue, 03-Aug-2010 09:25:03 GMT; path=/";
            var requester = new CookieWebRequester(new CookieWebResponder(cookieText));

            var browser = new BrowserEngine(requester);
            browser.Load("http://www.totaljobs.com/");

            Assert.That(browser.Headers[HttpResponseHeader.SetCookie], Is.EqualTo(cookieText));
        }

//        [Test]
//        public void ShouldSetMultipleCookies()
//        {
//            var cookies = new List<string>
//                              {
//                                  "Chocolate=Chip; expires=Tue, 03-Aug-2010 09:25:03 GMT; path=/",
//                                  "Tea=WhiteOneSugar; expires=Tue, 03-Aug-2010 09:25:03 GMT; path=/"
//                              };
//
//            var requester = new CookieWebRequester(new CookieWebResponder(cookies));
//
//            var browser = new BrowserEngine(requester);
//            browser.Load("http://www.totaljobs.com/");
//
//            
//            //Assert.That(browser.Headers[HttpResponseHeader.SetCookie], Is.EqualTo(string.Format("{0},{1}",cookies[0],cookies[1])));
//            Assert.That(browser.Cookies.GetCookieNamed("Chocolate").Value, Is.EqualTo("Chip"));
//        }
    }
}
