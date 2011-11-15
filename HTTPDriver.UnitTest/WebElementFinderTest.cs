using System.Collections.Generic;
using HTTPDriver.Browser.UnitTest;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HTTPDriver.UnitTest
{
    [TestFixture]
    public class WebElementFinderTest
    {
        [Test]
        public void FindElementById()
        {
            var finder = CreateElementFinder("<html><body><div id=\"page-body\">Hello world</div></body></html>");

            var div = finder.FindElement(By.Id("page-body"));
            AssertTag(div, "div", "Hello world");
        }

        [Test]
        public void FindElementsById()
        {
            // this isn't really valid, right?
            var finder = CreateElementFinder("<html><body>" +
                                             "<div id=\"page-body\">Hello world</div>>" +
                                             "<div id=\"page-body\">Hello world</div>" +
                                             "</body></html>");

            var divs = finder.FindElements(By.Id("page-body"));
            AssertTag(divs, "div", 2);
        }

        [Test]
        public void FindElementByClassName()
        {
            var finder = CreateElementFinder("<ul class=\"navigation\"><li>Item 1</li><li>Item 2</li></ul>");

            var list = finder.FindElement(By.ClassName("navigation"));
            AssertTag(list, "ul");
        }

        [Test]
        public void FindElementsByClassName()
        {
            var finder = CreateElementFinder("<ul><li class=\"nav-item\">Item 1</li><li class=\"nav-item\">Item 2</li></ul>");

            var list = finder.FindElements(By.ClassName("nav-item"));
            AssertTag(list, "li", 2);
        }

        [Test]
        public void FindElementByTagName()
        {
            var finder = CreateElementFinder("<html><body><p>elephant</p></body></html>");

            var paragraph = finder.FindElement(By.TagName("p"));
            AssertTag(paragraph, "p", "elephant");
        }

        [Test]
        public void FindElementsByTagName()
        {

            var finder = CreateElementFinder("<table><tr><td>foo</td><td>bar</td></tr></table>");

            var list = finder.FindElements(By.TagName("td"));
            AssertTag(list, "td", 2);
        }

        [Test]
        public void FindElementByCssSelector()
        {
            var finder = CreateElementFinder("<html><body><input type=\"button\"></body></html>");

            var button = finder.FindElement(By.CssSelector("input[type=button]"));
            AssertTag(button, "input");
        }

        [Test]
        public void FindElementsByCssSelector()
        {
            var finder = CreateElementFinder("<html><body>" +
                                             "<form><input type=\"text\" class=\"field\"><input type=\"button\" class=\"field\"></form>" +
                                             "</body></html>");

            var list = finder.FindElements(By.CssSelector(".field"));
            AssertTag(list, "input", 2);
        }

        [Test]
        public void FindElementByXPath()
        {
            var finder = CreateElementFinder("<html><body>" +
                                             "<a href=\"http://www.google.com\">google</a>" +
                                             "</body></html>");

            var link = finder.FindElement(By.XPath("//a[text()='google']"));
            AssertTag(link, "a", "google");
        }

        [Test]
        public void FindElementsByXPath()
        {
            var finder = CreateElementFinder("<table><tr><td>foo</td><td>bar</td></tr></table>");

            var list = finder.FindElements(By.XPath("//td"));
            AssertTag(list, "td", 2);
        }

        [Test]
        public void FindElementByLinkText()
        {
            var finder = CreateElementFinder("<a href=\"http://www.google.com\">google</a>" +
                                             "<a href=\"http://www.yahoo.com\">yahoo</a>" +
                                             "<a href=\"http://www.bing.com\">bing</a>");

            var link = finder.FindElement(By.LinkText("google"));
            AssertTag(link, "a", "google");
        }

        [Test]
        public void FindElementsByLinkText()
        {
            var finder = CreateElementFinder("<a href=\"http://www.google.com\">google</a>" +
                                             "<a href=\"http://www.yahoo.com\">yahoo</a>" +
                                             "<a href=\"http://www.google.com\">google</a>");

            var links = finder.FindElements(By.LinkText("google"));
            AssertTag(links, "a", "google", 2);
        }

        [Test]
        public void FindElementsByLinkTextShouldOnlyMatchEntireText()
        {
            var finder = CreateElementFinder("<a href=\"http://www.google.com\">link to google</a>");

            var link = finder.FindElement(By.LinkText("google"));
            Assert.That(link, Is.EqualTo(null));
        }

        [Test]
        public void FindElementByPartialLink()
        {
            var finder = CreateElementFinder("<a href=\"http://www.google.com\">link to google</a>");

            var link = finder.FindElement(By.PartialLinkText("google"));
            AssertTag(link, "a", "link to google");
        }

        [Test]
        public void FindElementsByPartialLinkText()
        {
            var finder = CreateElementFinder("<a href=\"http://www.google.com\">link to google</a>" +
                                             "<a href=\"http://www.yahoo.com\">link to yahoo</a>" +
                                             "<a href=\"http://www.bing.com\">link to.. microsoft google.. I mean bing</a>");

            var links = finder.FindElements(By.PartialLinkText("google"));
            AssertTag(links, "a", 2);
        }

        private WebElementFinder CreateElementFinder(string html)
        {
            var htmlNode = new HtmlNodeBuilder(html).Build();
            return new WebElementFinder(htmlNode,null);
        }

        private void AssertTag(IWebElement element, string tagName)
        {
            Assert.That(element.TagName, Is.EqualTo(tagName));
        }

        private void AssertTag(IWebElement element, string tagName, string text)
        {
            Assert.That(element.TagName, Is.EqualTo(tagName));
            Assert.That(element.Text, Is.EqualTo(text));
        }

        private void AssertTag(ICollection<IWebElement> list, string expectedTagName, int expectedElements)
        {
            Assert.That(list.Count, Is.EqualTo(expectedElements));
            foreach (var element in list)
                AssertTag(element, expectedTagName);
        }

        private void AssertTag(ICollection<IWebElement> list, string expectedTagName, string expectedText, int expectedElements)
        {
            Assert.That(list.Count, Is.EqualTo(expectedElements));
            foreach (var element in list)
                AssertTag(element, expectedTagName, expectedText);
        }
    }
}
