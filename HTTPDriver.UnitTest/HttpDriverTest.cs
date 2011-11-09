using HTTPDriver.UnitTest.Fakes;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HTTPDriver.UnitTest
{
    [TestFixture]
    public class HttpDriverTest
    {
        private HttpDriver _driver;

        private const string Url1 = "http://www.testurl1.com/";
        private const string Url2 = "http://www.testurl2.com/";
        private const string Url3 = "http://www.testurl3.com/";

        private HttpDriverFakeWebRequester _webRequester;

        [SetUp]
        public void BeforeEachTest()
        {
            _webRequester = new HttpDriverFakeWebRequester();
            _driver = new HttpDriver(_webRequester);
        }

        [Test]
        public void Initialise()
        {
            Assert.That(_driver, Is.InstanceOf<IWebDriver>());
        }

        [Test]
        public void Navigate()
        {
            var navigation = _driver.Navigate();

            Assert.That(navigation, Is.InstanceOf<Navigation>());
        }

        [Test]
        public void Title()
        {
            _webRequester.AddTestResponseString(Url1, new HtmlParser("<html><title>Test Title</title></html>"));
            _webRequester.AddTestResponseString(Url2, new HtmlParser("<html><title>Another Test Title</title></html>"));
            _webRequester.AddTestResponseString(Url3, new HtmlParser("<html></html>"));

            _driver.Navigate().GoToUrl(Url1);

            Assert.That(_driver.Title, Is.EqualTo("Test Title"));

            _driver.Navigate().GoToUrl(Url2);

            Assert.That(_driver.Title, Is.EqualTo("Another Test Title"));

            _driver.Navigate().GoToUrl(Url3);

            Assert.That(_driver.Title, Is.EqualTo(""));
        }
    }
}
