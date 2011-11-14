using System;
using HTTPDriver.Cookies;
using NUnit.Framework;

namespace HTTPDriver.UnitTest.Cookies
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

            Assert.That(cookie.Name, Is.EqualTo("Chocolate"));
            Assert.That(cookie.Value, Is.EqualTo(""));
            Assert.That(cookie.Expiry, Is.EqualTo(_expiry));
        }

        [Test]
        public void ParseCookieWithExpiryAndValue()
        {
            var cookie = CookieParser.ParseCookie("Chocolate=Chip; expires=Tue, 03-Aug-2010 09:25:03 GMT; path=/");

            Assert.That(cookie.Name, Is.EqualTo("Chocolate"));
            Assert.That(cookie.Value, Is.EqualTo("Chip"));
            Assert.That(cookie.Expiry, Is.EqualTo(_expiry));
        }

        [Test]
        public void ParseCookieWithNoExpiryAndNoValue()
        {
            var cookie = CookieParser.ParseCookie("Chocolate=; path=/");

            Assert.That(cookie.Name, Is.EqualTo("Chocolate"));
            Assert.That(cookie.Value, Is.EqualTo(""));
            Assert.That(cookie.Expiry, Is.Null);
        }

        [Test]
        public void ParseCookieWithNoExpiryAndValue()
        {
            var cookie = CookieParser.ParseCookie("Chocolate=Chip; path=/");

            Assert.That(cookie.Name, Is.EqualTo("Chocolate"));
            Assert.That(cookie.Value, Is.EqualTo("Chip"));
            Assert.That(cookie.Expiry, Is.Null);
        }

        [Test]
        public void ParseCookieWithEqualsInTheValue()
        {
            var cookie = CookieParser.ParseCookie("Cookie=Flavour=Chocolate&Chip; path=/");

            Assert.That(cookie.Name, Is.EqualTo("Cookie"));
            Assert.That(cookie.Value, Is.EqualTo("Flavour=Chocolate&Chip"));
            Assert.That(cookie.Expiry, Is.Null);
        }
    }
}
