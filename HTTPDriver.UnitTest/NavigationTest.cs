using System;
using NUnit.Framework;

namespace HTTPDriver.UnitTest
{
    public class NavigationTest
    {
        private HttpDriver _driver;
        private Navigation _navigation;

        private const string Url1 = @"http://www.testurl1.com/";
        private const string Url2 = @"http://www.testurl2.com";
        private const string Url3 = @"http://www.testurl3.com";

        [SetUp]
        public void BeforeEachTest()
        {
            _driver = new HttpDriver();
            _navigation = new Navigation(_driver);
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
            _navigation.GoToUrl(@"www.invalidtesturl.com");
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
