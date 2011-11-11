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
        private const string TotaljobsUrl = "http://www.totaljobs.com";

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

        [Test]
        public void PageSource()
        {
            //Given
            var originalPageSouce = "<html><title>Test Title</title></html>";
            _webRequester.AddTestResponseString(Url1, new HtmlParser(originalPageSouce));

            //When
            _driver.Navigate().GoToUrl(Url1);            
            var pageSource = _driver.PageSource;

            //Then
            Assert.That(pageSource,Is.EqualTo(originalPageSouce));
        }

        [Test]
        public void Url()
        {
            //Given
            _webRequester.AddTestResponseString(TotaljobsUrl, new HtmlParser("<html><title>Page 1</title></html>"));
            //When
            _driver.Navigate().GoToUrl(TotaljobsUrl);

            //Then
            Assert.That(_driver.Url, Is.EqualTo(TotaljobsUrl));
        }

        [Test]
        public void FindElement()
        {
            //Given
            _webRequester.AddTestResponseString(TotaljobsUrl, new HtmlParser("<html><title>Page 1</title><body><h1>The Header 1</h1></body></html>"));
            //When
            _driver.Navigate().GoToUrl(TotaljobsUrl);
            var foundElement = _driver.FindElement(By.TagName("h1"));

            //Then
            Assert.That(foundElement, Is.Not.Null);
            Assert.That(foundElement, Is.InstanceOf<IWebElement>());
            Assert.That(foundElement.TagName, Is.EqualTo("h1"));
            Assert.That(foundElement.Text, Is.EqualTo("The Header 1"));
        }

    }
}
