using System.Net;
using HTTPDriver.Browser;
using NUnit.Framework;

namespace HTTPDriver.FunctionalTests
{
    [TestFixture]
    public class RedirectTests
    {
        [Test]
        public void TemporaryRedirect()
        {
            var driver = new HttpDriver(new WebRequester()
                                .AutomaticallyFollowRedirects(false));

            driver.Navigate().GoToUrl("http://localhost:9001/TestSite/TemporaryRedirectToAnotherPage.aspx");

            Assert.That(driver.Title, Is.EqualTo("Object moved"));
            Assert.That(driver.Url, Is.EqualTo("http://localhost:9001/TestSite/TemporaryRedirectToAnotherPage.aspx"));
        }

        [Test]
        public void PermanentRedirect()
        {
            var driver = new HttpDriver(new WebRequester()
                                .AutomaticallyFollowRedirects(false));

            driver.Navigate().GoToUrl("http://localhost:9001/TestSite/PermanentRedirectToAnotherPage.aspx");

            Assert.That(driver.Url, Is.EqualTo("http://localhost:9001/TestSite/PermanentRedirectToAnotherPage.aspx"));
        }

        [Test]
        public void TemporaryRedirectWithAutoFollowRedirects()
        {
            var driver = new HttpDriver(new WebRequester()
                                .AutomaticallyFollowRedirects(true));

            driver.Navigate().GoToUrl("http://localhost:9001/TestSite/TemporaryRedirectToAnotherPage.aspx");

            Assert.That(driver.Title, Is.EqualTo("Another Page"));
            Assert.That(driver.Url, Is.StringContaining("/TestSite/AnotherPage.aspx"));
        }

        [Test]
        public void PermanentRedirectWithAutoFollowRedirects()
        {
            var driver = new HttpDriver(new WebRequester()
                                .AutomaticallyFollowRedirects(true));

            driver.Navigate().GoToUrl("http://localhost:9001/TestSite/PermanentRedirectToAnotherPage.aspx");

            Assert.That(driver.Title, Is.EqualTo("Another Page"));
            Assert.That(driver.Url, Is.StringContaining("/TestSite/AnotherPage.aspx"));
        }
    }
}