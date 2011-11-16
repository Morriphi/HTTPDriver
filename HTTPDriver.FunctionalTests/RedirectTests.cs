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

            Assert.That(driver.StatusCode, Is.EqualTo(HttpStatusCode.Found)); 
            Assert.That(driver.Title, Is.EqualTo("Object moved"));            
        }

        [Test]
        public void PermanentRedirect()
        {
            var driver = new HttpDriver(new WebRequester()
                                .AutomaticallyFollowRedirects(false));

            driver.Navigate().GoToUrl("http://localhost:9001/TestSite/PermanentRedirectToAnotherPage.aspx");

            Assert.That(driver.StatusCode, Is.EqualTo(HttpStatusCode.MovedPermanently));
        }

        [Test]
        public void TemporaryRedirectWithAutoFollowRedirects()
        {
            var driver = new HttpDriver(new WebRequester()
                                .AutomaticallyFollowRedirects(true));

            driver.Navigate().GoToUrl("http://localhost:9001/TestSite/TemporaryRedirectToAnotherPage.aspx");

            Assert.That(driver.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(driver.Title, Is.EqualTo("Another Page"));
        }

        [Test]
        public void PermanentRedirectWithAutoFollowRedirects()
        {
            var driver = new HttpDriver(new WebRequester()
                                .AutomaticallyFollowRedirects(true));

            driver.Navigate().GoToUrl("http://localhost:9001/TestSite/PermanentRedirectToAnotherPage.aspx");

            Assert.That(driver.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(driver.Title, Is.EqualTo("Another Page"));
        }
    }
}