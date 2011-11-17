using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest
{
    [TestFixture]
    public class HistoryTest
    {
        private History _history;

        private const string Url1 = "Url 1";
        private const string Url2 = "Url 2";
        private const string Url3 = "Url 3";

        [SetUp]
        public void BeforeEachTest()
        {
            _history = new History();
        }

        [Test]
        public void CurrentUrl()
        {
            _history.Add(Url1);

            Assert.That(_history.CurrentUrl(), Is.EqualTo(Url1));

            _history.Add(Url2);

            Assert.That(_history.CurrentUrl(), Is.EqualTo(Url2));
        }

        [Test]
        public void Back()
        {
            _history.Add(Url1);
            _history.Add(Url2);
            _history.Add(Url3);

            Assert.That(_history.CurrentUrl(), Is.EqualTo(Url3));

            _history.Back();

            Assert.That(_history.CurrentUrl(), Is.EqualTo(Url2));

            _history.Back();

            Assert.That(_history.CurrentUrl(), Is.EqualTo(Url1));
        }

        [Test]
        public void Forward()
        {
            _history.Add(Url1);
            _history.Add(Url2);
            _history.Add(Url3);

            _history.SetCurrent(Url1);

            Assert.That(_history.CurrentUrl(), Is.EqualTo(Url1));

            _history.Forward();

            Assert.That(_history.CurrentUrl(), Is.EqualTo(Url2));

            _history.Forward();

            Assert.That(_history.CurrentUrl(), Is.EqualTo(Url3));
        }

        [Test]
        [ExpectedException(typeof(History.NoPreviousUrl))]
        public void ShouldHaveNoPreviousPage()
        {
            _history.Add(Url1);

            _history.Back();
        }

        [Test]
        [ExpectedException(typeof(History.NoNextUrl))]
        public void ShouldHaveNoNextPage()
        {
            _history.Add(Url1);

            _history.Forward();
        }

        [Test]
        [ExpectedException(typeof(History.NoPreviousUrl))]
        public void BackEmptyHistory()
        {
            _history.Back();
        }

        [Test]
        [ExpectedException(typeof(History.NoNextUrl))]
        public void ForwardEmptyHistory()
        {
            _history.Forward();
        }
    }
}
