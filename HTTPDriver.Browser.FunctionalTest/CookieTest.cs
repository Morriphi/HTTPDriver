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
        public void ShouldHaveCookies()
        {
            _engine.Load("http://localhost:9001/TestSite/SetCookie.aspx");

            Assert.That(_engine.Cookies.AllCookies.Count, Is.EqualTo(2));
            Assert.That(_engine.Cookies.GetCookieNamed("Tea"), Is.Not.Null);
            Assert.That(_engine.Cookies.GetCookieNamed("Coffee"), Is.Not.Null);
            Assert.That(_engine.Cookies.GetCookieNamed("Tea").Value, Is.EqualTo("LoveOne"));
            Assert.That(_engine.Cookies.GetCookieNamed("Coffee").Value, Is.EqualTo("BlackStrongNoSugar"));
        }
    }
}
