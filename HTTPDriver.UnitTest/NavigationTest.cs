using System;
using HTTPDriver.Browser.UnitTest.Fakes;
using NUnit.Framework;

namespace HTTPDriver.UnitTest
{
    public class NavigationTest
    {
        private HttpDriver _driver;
        private Navigation _navigation;

        private const string Url1 = @"http://www.testurl1.com/";
        private const string Url2 = @"http://www.testurl2.com/";
        private const string Url3 = @"http://www.testurl3.com/";

        private FakeWebRequester _webRequester;

        [SetUp]
        public void BeforeEachTest()
        {
            _webRequester = new FakeWebRequester("");

            _driver = new HttpDriver(_webRequester);
            _navigation = new Navigation(_driver);
        }

        [Test]
        [TestCase("http://www.test.com/Default.aspx", "http://www.test.com/Sub/RelativePage.aspx")]
        [TestCase("http://www.test.com/1/2",  "http://www.test.com/1/2/Sub/RelativePage.aspx")]
        [TestCase("http://www.test.com/1/2/", "http://www.test.com/1/2/Sub/RelativePage.aspx")]
        [TestCase("http://www.test.com/", "http://www.test.com/Sub/RelativePage.aspx")]
        [TestCase("http://www.test.com/", "http://www.test.com/Sub/RelativePage.aspx")]
        [TestCase("http://www.test.com/1/2/3/Default.aspx", "http://www.test.com/1/2/3/Sub/RelativePage.aspx")]
        [TestCase("http://www.test.com/1/2/3/Default.aspx?id=1", "http://www.test.com/1/2/3/Sub/RelativePage.aspx")]
        [TestCase("http://www.test.com/query?id=1", "http://www.test.com/Sub/RelativePage.aspx")]
        public void GoToUrlShouldHandleRelativeUrls(string initalUrl, string expectedUrl)
        {
            _navigation.GoToUrl(initalUrl);

            Assert.That(_driver.Url, Is.EqualTo(initalUrl));

            _navigation.GoToUrl("Sub/RelativePage.aspx");

            Assert.That(_driver.Url, Is.EqualTo(expectedUrl));
        }

        [Test]
        public void ShouldGoToUrlWithGivenStringUrl()
        {
            _navigation.GoToUrl(Url1);

            Assert.That(_driver.Url, Is.EqualTo(Url1));
        }

        [Test]
        public void ShouldGoToUrlWithGivenUri()
        {
            _navigation.GoToUrl(new Uri(Url1));

            Assert.That(_driver.Url, Is.EqualTo(Url1));
        }

        [Test]
        [ExpectedException(typeof(UriFormatException))]
        public void InvalidUrl()
        {
            _navigation.GoToUrl(@"www.invalidtesturl:\\.com");
        }

        [Test]
        public void ShouldGoBackToPreviousUrl()
        {
            _navigation.GoToUrl(Url1);
            _navigation.GoToUrl(Url2);
            _navigation.GoToUrl(Url3);

            Assert.That(_driver.Url, Is.EqualTo(Url3));

            _navigation.Back();

            Assert.That(_driver.Url, Is.EqualTo(Url2));

            _navigation.Back();

            Assert.That(_driver.Url, Is.EqualTo(Url1));
        }

        [Test]
        public void ShouldGoForwardToNextUrl()
        {
            _navigation.GoToUrl(Url1);
            _navigation.GoToUrl(Url2);
            _navigation.GoToUrl(Url3);

            _navigation.Back();
            _navigation.Back();

            Assert.That(_driver.Url, Is.EqualTo(Url1));

            _navigation.Forward();

            Assert.That(_driver.Url, Is.EqualTo(Url2));

            _navigation.Forward();

            Assert.That(_driver.Url, Is.EqualTo(Url3));
        }
    }
}
