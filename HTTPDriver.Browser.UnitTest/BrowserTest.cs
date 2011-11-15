using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest
{
    [TestFixture]
    public class BrowserTest
    {
        [Test]
        public void LoadsUrl()
        {
            var location = "http://www.totaljobs.com/";
            var requester = new WebRequesterFake("<p>Hello world</p>");

            var browser = new BrowserEngine(requester);
            browser.Load(location);

            Assert.That(browser.Location.ToString(), Is.EqualTo(location));
            Assert.That(browser.Page.SelectSingleNode("//p").Text(), Is.EqualTo("Hello world"));

        }
    }
}
