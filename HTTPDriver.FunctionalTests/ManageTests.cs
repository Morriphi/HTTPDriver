using HTTPDriver.Browser;
using NUnit.Framework;

namespace HTTPDriver.FunctionalTests
{
    [TestFixture]
    public class ManageTests
    {
        private HttpDriver _driver;

        [SetUp]
        public void BeforeEachTest()
        {
            _driver = new HttpDriver(new WebRequester());
        }

        [Test]
        public void ShouldBeAbleToNavigateToWebSite()
        {
            _driver.Navigate().GoToUrl("http://localhost:9001/TestSite/SetCookie.aspx");

            Assert.That(_driver.Manage(), Is.InstanceOf<Manage>());

            var cookieJar = _driver.Manage().Cookies;   
            Assert.That(cookieJar.GetCookieNamed("Tea").Value, Is.EqualTo("LoveOne"));
        }
    }
}