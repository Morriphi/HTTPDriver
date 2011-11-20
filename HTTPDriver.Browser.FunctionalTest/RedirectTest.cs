using System.Net;
using NUnit.Framework;

namespace HTTPDriver.Browser.FunctionalTest
{
    [TestFixture]
    public class RedirectTest
    {
        private BrowserEngine _browser;

        [Test]
        public void TemporaryRedirect()
        {
            _browser = new BrowserEngine(new WebRequester()
                                             .AutomaticallyFollowRedirects(false));

            _browser.Load("http://localhost:9001/TestSite/TemporaryRedirectToAnotherPage.aspx");

            Assert.That(_browser.ResponseStatusCode, Is.EqualTo(HttpStatusCode.Found));
            Assert.That(_browser.Page.Title(), Is.EqualTo("Object moved"));
            Assert.That(_browser.Location.AbsoluteUri, 
                Is.EqualTo("http://localhost:9001/TestSite/TemporaryRedirectToAnotherPage.aspx"));
        }

        [Test]
        public void PermanentRedirect()
        {
            _browser = new BrowserEngine(new WebRequester()
                                .AutomaticallyFollowRedirects(false));

            _browser.Load("http://localhost:9001/TestSite/PermanentRedirectToAnotherPage.aspx");

            Assert.That(_browser.ResponseStatusCode, Is.EqualTo(HttpStatusCode.MovedPermanently));
            Assert.That(_browser.Location.AbsoluteUri, 
                Is.EqualTo("http://localhost:9001/TestSite/PermanentRedirectToAnotherPage.aspx"));
        }

        [Test]
        public void TemporaryRedirectWithAutoFollowRedirects()
        {
            _browser = new BrowserEngine(new WebRequester()
                                .AutomaticallyFollowRedirects(true));

            _browser.Load("http://localhost:9001/TestSite/TemporaryRedirectToAnotherPage.aspx");

            Assert.That(_browser.ResponseStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(_browser.Page.Title(), Is.EqualTo("Another Page"));
            Assert.That(_browser.Location.AbsoluteUri, Is.StringContaining("/TestSite/AnotherPage.aspx"));
        }

        [Test]
        public void PermanentRedirectWithAutoFollowRedirects()
        {
            _browser = new BrowserEngine(new WebRequester()
                                .AutomaticallyFollowRedirects(true));

            _browser.Load("http://localhost:9001/TestSite/PermanentRedirectToAnotherPage.aspx");

            Assert.That(_browser.ResponseStatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(_browser.Page.Title(), Is.EqualTo("Another Page"));
            Assert.That(_browser.Location.AbsoluteUri, Is.StringContaining("/TestSite/AnotherPage.aspx"));
        }
    }
}
