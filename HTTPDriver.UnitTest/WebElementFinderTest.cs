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
            var htmlNode = new HtmlNodeBuilder("<html><body><div id=\"page-body\">Hello world</div></body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var div = document.FindElement(By.Id("page-body"));

            Assert.That(div.TagName, Is.EqualTo("div"));
            Assert.That(div.Text, Is.EqualTo("Hello world"));
        }

        [Test]
        public void FindElementByClassName()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                                           "<ul class=\"navigation\"><li>Item 1</li><li>Item 2</li></ul>" +
                                           "</body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var list = document.FindElement(By.ClassName("navigation"));

            Assert.That(list.TagName, Is.EqualTo("ul"));
        }

        [Test]
        public void FindElementsByClassName()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                                           "<ul><li class=\"nav-item\">Item 1</li><li class=\"nav-item\">Item 2</li></ul>" +
                                           "</body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var list = document.FindElements(By.ClassName("nav-item"));

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].TagName, Is.EqualTo("li"));
            Assert.That(list[1].TagName, Is.EqualTo("li"));
        }

        [Test]
        public void FindElementByTagName()
        {
            var paragraphText = "elephant";
            var tagName = "p";

            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                               string.Format("<p>{0}</p>", paragraphText) +
                               "</body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var paragraph = document.FindElement(By.TagName(tagName));

            Assert.That(paragraph.TagName, Is.EqualTo(tagName));
            Assert.That(paragraph.Text, Is.EqualTo(paragraphText));
        }

        [Test]
        public void FindElementsByTagName()
        {
            var tagName = "td";

            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                               "<table><tr><td>foo</td><td>bar</td></tr></table>" +
                               "</body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var list = document.FindElements(By.TagName(tagName));

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].TagName, Is.EqualTo(tagName));
            Assert.That(list[1].TagName, Is.EqualTo(tagName));
        }

        [Test]
        public void FindElementByCssSelector()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body><input type=\"button\"></body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var list = document.FindElement(By.CssSelector("input[type=button]"));

            Assert.That(list.TagName, Is.EqualTo("input"));
        }

        [Test]
        public void FindElementsByCssSelector()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                                           "<form><input type=\"text\" class=\"field\"><input type=\"button\" class=\"field\"></form>" +
                                           "</body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var list = document.FindElements(By.CssSelector(".field"));

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].TagName, Is.EqualTo("input"));
            Assert.That(list[1].TagName, Is.EqualTo("input"));
        }

        [Test]
        public void FindElementByXPath()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                                           "<a href=\"http://www.google.com\">google</a>" +
                                           "</body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var link = document.FindElement(By.XPath("//a[text()='google']"));

            Assert.That(link.TagName, Is.EqualTo("a"));
            Assert.That(link.Text, Is.EqualTo("google"));
        }

        [Test]
        public void FindElementsByXPath()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                                           "<table><tr><td>foo</td><td>bar</td></tr></table>" +
                                           "</body></html>").Build();

            var document = new WebElementFinder(htmlNode);
            var list = document.FindElements(By.XPath("//td"));

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].TagName, Is.EqualTo("td"));
            Assert.That(list[0].Text, Is.EqualTo("foo"));
            Assert.That(list[1].TagName, Is.EqualTo("td"));
            Assert.That(list[1].Text, Is.EqualTo("bar"));
        }
    }
}
