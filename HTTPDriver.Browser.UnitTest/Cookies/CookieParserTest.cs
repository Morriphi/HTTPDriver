using System;
using HTTPDriver.Browser.Cookies;
using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest.Cookies
{
    [TestFixture]
    public class CookieParserTest
    {
        private DateTime _expiry;

        [SetUp]
        public void BeforeEachTest()
        {
            _expiry = new DateTime(2010, 8, 3, 10, 25, 3);
        }

        [Test]
        public void ParseCookieWithExpiryAndNoValue()
        {
            var cookie = CookieParser.ParseCookie("Chocolate=; expires=Tue, 03-Aug-2010 09:25:03 GMT; path=/");

            Assert.That((object) cookie.Name, Is.EqualTo("Chocolate"));
            Assert.That((object) cookie.Value, Is.EqualTo(""));
            Assert.That((object) cookie.Expires, Is.EqualTo(_expiry));
        }

        [Test]
        public void ParseCookieWithExpiryAndValue()
        {
            var cookie = CookieParser.ParseCookie("Chocolate=Chip; expires=Tue, 03-Aug-2010 09:25:03 GMT; path=/");

            Assert.That((object) cookie.Name, Is.EqualTo("Chocolate"));
            Assert.That((object) cookie.Value, Is.EqualTo("Chip"));
            Assert.That((object) cookie.Expires, Is.EqualTo(_expiry));
        }

        [Test]
        public void ParseCookieWithNoExpiryAndNoValue()
        {
            var cookie = CookieParser.ParseCookie("Chocolate=; path=/");

            Assert.That((object) cookie.Name, Is.EqualTo("Chocolate"));
            Assert.That((object) cookie.Value, Is.EqualTo(""));
            Assert.That((object) cookie.Expires, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void ParseCookieWithNoExpiryAndValue()
        {
            var cookie = CookieParser.ParseCookie("Chocolate=Chip; path=/");

            Assert.That((object) cookie.Name, Is.EqualTo("Chocolate"));
            Assert.That((object) cookie.Value, Is.EqualTo("Chip"));
            Assert.That((object) cookie.Expires, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void ParseCookieWithEqualsInTheValue()
        {
            var cookie = CookieParser.ParseCookie("Cookie=Flavour=Chocolate&Chip; path=/");

            Assert.That((object) cookie.Name, Is.EqualTo("Cookie"));
            Assert.That((object) cookie.Value, Is.EqualTo("Flavour=Chocolate&Chip"));
            Assert.That((object) cookie.Expires, Is.EqualTo(DateTime.MinValue));
        }
    }
}
