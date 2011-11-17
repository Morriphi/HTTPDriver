using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest
{
    [TestFixture]
    public class BrowserHistoryTest
    {
        private BrowserEngine _browser;

        private const string Url1 = "http://testurl1.com/";
        private const string Url2 = "http://testurl2.com/";
        private const string Url3 = "http://testurl3.com/";

        [SetUp]
        public void BeforeEachTest()
        {
            _browser = new BrowserEngine(new WebRequesterFake(""));
        }

        [Test]
        public void CurrentLocation()
        {
            _browser.Load(Url1);

            Assert.That(_browser.Location.ToString(), Is.EqualTo(Url1));

            _browser.Load(Url2);

            Assert.That(_browser.Location.ToString(), Is.EqualTo(Url2));
        }

        [Test]
        public void Back()
        {
            _browser.Load(Url1);
            _browser.Load(Url2);
            _browser.Load(Url3);

            Assert.That(_browser.Location.ToString(), Is.EqualTo(Url3));

            _browser.Back();

            Assert.That(_browser.Location.ToString(), Is.EqualTo(Url2));

            _browser.Back();

            Assert.That(_browser.Location.ToString(), Is.EqualTo(Url1));
        }

        [Test]
        public void Forward()
        {
            _browser.Load(Url1);
            _browser.Load(Url2);
            _browser.Load(Url3);

            _browser.SetCurrent(Url1);

            Assert.That(_browser.Location.ToString(), Is.EqualTo(Url1));

            _browser.Forward();

            Assert.That(_browser.Location.ToString(), Is.EqualTo(Url2));

            _browser.Forward();

            Assert.That(_browser.Location.ToString(), Is.EqualTo(Url3));
        }

        [Test]
        [ExpectedException(typeof(History.NoPreviousUrl))]
        public void ShouldHaveNoPreviousPage()
        {
            _browser.Load(Url1);

            _browser.Back();
        }

        [Test]
        [ExpectedException(typeof(History.NoNextUrl))]
        public void ShouldHaveNoNextPage()
        {
            _browser.Load(Url1);

            _browser.Forward();
        }

        [Test]
        [ExpectedException(typeof(History.NoPreviousUrl))]
        public void BackEmptyHistory()
        {
            _browser.Back();
        }

        [Test]
        [ExpectedException(typeof(History.NoNextUrl))]
        public void ForwardEmptyHistory()
        {
            _browser.Forward();
        }
    }
}
