using HTTPDriver.FunctionalTests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HTTPDriver.FunctionalTests
{
    public class NavigationTests : HttpTestSiteFixture
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
            _driver.Navigate().GoToUrl("http://localhost:9001/TestSite");

            Assert.That(_driver.Title, Is.EqualTo("Test Site"));
        }

        [Test]
        public void ShouldBeAbleToFindElementById()
        {
            _driver.Navigate().GoToUrl("http://localhost:9001/TestSite");

            Assert.That(_driver.FindElement(By.Id("page-body")).TagName, Is.EqualTo("div"));
            Assert.That(_driver.FindElement(By.Id("page-body")).Text, Is.EqualTo("Hello world"));
        }

        [Test]
        public void ShouldBeAbleToFindElementsByClass()
        {
            _driver.Navigate().GoToUrl("http://localhost:9001/TestSite");

            Assert.That(_driver.FindElements(By.ClassName("nav-item")).Count, Is.EqualTo(2));
        }
    }
}
