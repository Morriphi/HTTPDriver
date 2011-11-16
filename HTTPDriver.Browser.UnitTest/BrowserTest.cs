using System.Net;
using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest
{
    [TestFixture]
    public class BrowserTest
    {
        [Test]
        public void LoadsUrl()
        {
            const string location = "http://www.totaljobs.com/";
            var requester = new WebRequesterFake("<p>Hello world</p>");

            var browser = new BrowserEngine(requester);
            browser.Load(location);

            Assert.That(browser.Location.ToString(), Is.EqualTo(location));
            Assert.That(browser.Page, Is.InstanceOf<Page>());
            Assert.That(browser.Page.SelectSingleNodeText("//p"), Is.EqualTo("Hello world"));
            Assert.That(browser.ResponseStatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
