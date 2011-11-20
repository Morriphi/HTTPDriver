using System;
using System.Net;
using NUnit.Framework;

namespace HTTPDriver.Browser.FunctionalTest
{
    [TestFixture]
    public class CookieTest
    {
        private BrowserEngine _engine;

        [SetUp]
        public void BeforeEachTest()
        {
            _engine = new BrowserEngine(new WebRequester());
        }

        [Test]
        public void ShouldGetCookies()
        {
            _engine.Load("http://localhost:9001/TestSite/SetCookie.aspx");

            Assert.That(_engine.Cookies.AllCookies.Count, Is.EqualTo(2));

            Assert.That(_engine.Cookies.GetCookieNamed("Tea"), Is.Not.Null, "Tea");
            Assert.That(_engine.Cookies.GetCookieNamed("Coffee"), Is.Not.Null, "Coffee");

            Assert.That(_engine.Cookies.GetCookieNamed("Tea").Value, Is.EqualTo("LoveOne"), "Tea Value");
            Assert.That(_engine.Cookies.GetCookieNamed("Coffee").Value, Is.EqualTo("BlackStrongNoSugar"), "Coffee Value");

            Assert.That(_engine.Cookies.GetCookieNamed("Tea").Expired, Is.EqualTo(true), "Tea Expired");
            Assert.That(_engine.Cookies.GetCookieNamed("Coffee").Expired, Is.EqualTo(false), "Coffee Expired");

            Assert.That(_engine.Cookies.GetCookieNamed("Tea").Expires.Date, 
                Is.EqualTo(DateTime.Now.AddDays(-1).Date));
        }

        [Test]
        public void ShouldSetCookies()
        {
            _engine.AddCookie(new Cookie("CookieSetByHTTPDriverBrowserTest", ""));
            _engine.Load("http://localhost:9001/TestSite/GetCookie.aspx");

            Assert.That(_engine.Page.Html(), 
                Is.StringContaining("This message will be display if cookie was set by HTTPDriver Browser"));
        }
    }
}
