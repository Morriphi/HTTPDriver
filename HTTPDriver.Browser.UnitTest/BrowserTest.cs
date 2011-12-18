using System;
using System.Net;
using HTTPDriver.Browser.UnitTest.Fakes;
using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest
{
    [TestFixture]
    public class BrowserTest
    {
        private FakeWebRequester _requester;
        private BrowserEngine _browser;

        [SetUp]
        public void BeforeEachTest()
        {
            _requester = new FakeWebRequester("<p>Hello world</p>");
            _browser = new BrowserEngine(_requester);
        }

        [Test]
        public void LoadsUrl()
        {
            const string location = "http://www.totaljobs.com/";
       
            _browser.Load(location);

            Assert.That(_browser.Location.ToString(), Is.EqualTo(location));
            Assert.That(_browser.Page, Is.InstanceOf<Page>());
            Assert.That(_browser.Page.SelectSingleNodeText("//p"), Is.EqualTo("Hello world"));
            Assert.That(_browser.ResponseStatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ShouldSetCookie()
        {
            var cookie = new Cookie("ChocolateChip", "", "", "");

            _requester.AddCookie(cookie);

            var browser = new BrowserEngine(_requester);
            browser.Load("http://www.totaljobs.com/");

            Assert.That(browser.Cookies.AllCookies.Count, Is.EqualTo(1));
            Assert.That(browser.Cookies.AllCookies[0].Name, Is.EqualTo(cookie.Name));
        }

        [Test]
        public void ShouldSetMultipleCookies()
        {
            var chocolateChip = new Cookie("ChocolateChip", "", "", "");
            var teaWhiteOneSugar = new Cookie("TeaWhiteNoSugar", "", "", "");

            _requester.AddCookie(chocolateChip);
            _requester.AddCookie(teaWhiteOneSugar);

            _browser.Load("http://www.totaljobs.com/");

            Assert.That(_browser.Cookies.AllCookies.Count, Is.EqualTo(2));
            Assert.That(_browser.Cookies.AllCookies[0].Name, Is.EqualTo(chocolateChip.Name));
            Assert.That(_browser.Cookies.AllCookies[1].Name, Is.EqualTo(teaWhiteOneSugar.Name));
        }

        [Test]
        public void ShouldSetExpiredCookie()
        {
            var chocolateChip = new Cookie("ChocolateChip", "", "", "")
                                    { Expired = true, Expires = DateTime.Now.AddDays(-1) };

            _requester.AddCookie(chocolateChip);

            _browser.Load("http://www.totaljobs.com/");

            Assert.That(_browser.Cookies.AllCookies.Count, Is.EqualTo(1));
            Assert.That(_browser.Cookies.AllCookies[0].Name, Is.EqualTo(chocolateChip.Name));
            Assert.That(_browser.Cookies.AllCookies[0].Expired, Is.EqualTo(chocolateChip.Expired));
            Assert.That(_browser.Cookies.AllCookies[0].Expires, Is.EqualTo(chocolateChip.Expires));
        }

        [Test]
        public void SubmitFormUsingPost()
        {
            var browser = new BrowserEngine(new FakeWebRequester("<html><form method=\"post\" action=\"http://totaljobs.com/results\"><input type=\"submit\" id=\"save-button\" value=\"save\"></form></html>"));
            browser.Load("http://totaljobs.com/");
            var saveButton = browser.Page.FindById("save-button");
            saveButton.Click();

            Assert.That(browser.Location.ToString(), Is.EqualTo("http://totaljobs.com/results"));
        }


        [Test, Ignore]
        public void SubmitFormWithRelativeUrl()
        {
            var browser = new BrowserEngine(new FakeWebRequester("<html><form method=\"post\" action=\"/results\"><input type=\"submit\" id=\"save-button\" value=\"save\"></form></html>"));
            browser.Load("http://totaljobs.com/");
            var saveButton = browser.Page.FindById("save-button");
            saveButton.Click();

            Assert.That(browser.Location.ToString(), Is.EqualTo("http://totaljobs.com/results"));
        }
    }
}
