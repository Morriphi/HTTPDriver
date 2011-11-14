using NUnit.Framework;
using OpenQA.Selenium;

namespace HTTPDriver.FunctionalTests
{
    [TestFixture]
    public class NavigationTests
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

        [Test]
        public void ShouldBeAbleToFollowSimpleHyperlink()
        {
            _driver.Navigate().GoToUrl("http://localhost:9001/TestSite");

            IWebElement hyperlink = _driver.FindElement(By.CssSelector("#linkToAnotherPage"));

            hyperlink.Click();

            Assert.That(_driver.Url, Is.EqualTo("http://localhost:9001/TestSite/AnotherPage.aspx"));

            IWebElement element = _driver.FindElement(By.CssSelector("#page-body"));

            Assert.That(element.Text, Is.EqualTo("This is another page."));
        }
    }
}
