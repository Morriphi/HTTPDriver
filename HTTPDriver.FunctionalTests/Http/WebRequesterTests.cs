using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTPDriver.Browser;
using HtmlAgilityPack;
using NUnit.Framework;

namespace HTTPDriver.FunctionalTests.Http
{
    [TestFixture]
    public class WebRequesterTests : HttpTestSiteFixture
    {
        [Test]
        public void CanGetWebpage()
        {
            var requester = new WebRequester();
            HtmlNode document = requester.Get("http://localhost:9001/TestSite").Page;

            Assert.That(document.SelectSingleNode("//h1").Text(), Is.EqualTo("Test Page"));
        }

        [Test]
        public void CanPostToWebpage()
        {
            var requester = new WebRequester();

            var postdata = new Dictionary<string, string> {{"hello", "world"}};
            var document = requester.Post("http://localhost:9001/TestSite/Forms/post-data.aspx", postdata).Page;

            var html = document.InnerHtml;
            Assert.That(html, Is.StringContaining("hello"));
            Assert.That(html, Is.StringContaining("world"));
        }

    }
}
