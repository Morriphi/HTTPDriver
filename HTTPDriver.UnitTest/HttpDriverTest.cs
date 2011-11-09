using NUnit.Framework;
using OpenQA.Selenium;

namespace HTTPDriver.UnitTest
{
    [TestFixture]
    public class HttpDriverTest
    {
        private HttpDriver _driver;

        [SetUp]
        public void BeforeEachTest()
        {
            _driver = new HttpDriver();   
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
    }
}
