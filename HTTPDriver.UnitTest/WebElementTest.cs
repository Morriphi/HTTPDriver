using HtmlAgilityPack;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HTTPDriver.UnitTest
{
    public class WebElementTest
    {
        

        private HtmlNode GetHtmlNodeFrom(string nodeHtml)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(nodeHtml);
            return htmlDocument.DocumentNode;
        }

        [Test] 
        public void Text()
        {

            var htmlNode = GetHtmlNodeFrom("<div>Hello World</div>");

            var element = new WebElement(htmlNode);
            var textInAnElement = element.Text;

            Assert.That(textInAnElement, Is.EqualTo("Hello World"));

        }

        [Test]
        public void TagName()
        {
            var htmlNode = GetHtmlNodeFrom("<div>Hello DIV Tag</div>");

            var element = new WebElement(htmlNode);

            Assert.That(element.TagName, Is.EqualTo("div"));
        }

        [Test]
        public void FindElementById()
        {
            var htmlNode = GetHtmlNodeFrom("<html><body><div id=\"page-body\">Hello world</div></body></html>");

            var document = new WebElement(htmlNode);
            var div = document.FindElement(By.Id("page-body"));

            Assert.That(div.TagName, Is.EqualTo("div"));
            Assert.That(div.Text, Is.EqualTo("Hello world"));
        }

        [Test]
        public void FindElementByClassName()
        {
            var htmlNode = GetHtmlNodeFrom("<html><body>" +
                                           "<ul class=\"navigation\"><li>Item 1</li><li>Item 2</li></ul>" +
                                           "</body></html>");

            var document = new WebElement(htmlNode);
            var list = document.FindElement(By.ClassName("navigation"));

            Assert.That(list.TagName, Is.EqualTo("ul"));
        }

        [Test]
        public void FindElementsByClassName()
        {
            var htmlNode = GetHtmlNodeFrom("<html><body>" +
                                           "<ul><li class=\"nav-item\">Item 1</li><li class=\"nav-item\">Item 2</li></ul>" +
                                           "</body></html>");

            var document = new WebElement(htmlNode);
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

            var htmlNode = GetHtmlNodeFrom("<html><body>" +
                               string.Format("<p>{0}</p>", paragraphText) +
                               "</body></html>");

            var document = new WebElement(htmlNode);
            var paragraph = document.FindElement(By.TagName(tagName));

            Assert.That(paragraph.TagName, Is.EqualTo(tagName));
            Assert.That(paragraph.Text, Is.EqualTo(paragraphText));
        }

        [Test]
        public void FindElementsByTagName()
        {
            var tagName = "td";

            var htmlNode = GetHtmlNodeFrom("<html><body>" +
                               "<table><tr><td>foo</td><td>bar</td></tr></table>" +
                               "</body></html>");

            var document = new WebElement(htmlNode);
            var list = document.FindElements(By.TagName(tagName));

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].TagName, Is.EqualTo(tagName));
            Assert.That(list[1].TagName, Is.EqualTo(tagName));
        }

        [Test]
        public void FindElementByCssSelector()
        {
            var htmlNode = GetHtmlNodeFrom("<html><body><input type=\"button\"></body></html>");

            var document = new WebElement(htmlNode);
            var list = document.FindElement(By.CssSelector("input[type=button]"));

            Assert.That(list.TagName, Is.EqualTo("input"));
        }

        [Test]
        public void FindElementsByCssSelector()
        {
            var htmlNode = GetHtmlNodeFrom("<html><body>" +
                                           "<form><input type=\"text\" class=\"field\"><input type=\"button\" class=\"field\"></form>" +
                                           "</body></html>");

            var document = new WebElement(htmlNode);
            var list = document.FindElements(By.CssSelector(".field"));

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].TagName, Is.EqualTo("input"));
            Assert.That(list[1].TagName, Is.EqualTo("input"));
        }

        [Test]
        public void FindElementByXPath()
        {
            var htmlNode = GetHtmlNodeFrom("<html><body>" +
                                           "<a href=\"http://www.google.com\">google</a>" +
                                           "</body></html>");

            var document = new WebElement(htmlNode);
            var link = document.FindElement(By.XPath("//a[text()='google']"));

            Assert.That(link.TagName, Is.EqualTo("a"));
            Assert.That(link.Text, Is.EqualTo("google"));
        }

        [Test]
        public void FindElementsByXPath()
        {
            var htmlNode = GetHtmlNodeFrom("<html><body>" +
                                           "<table><tr><td>foo</td><td>bar</td></tr></table>" +
                                           "</body></html>");

            var document = new WebElement(htmlNode);
            var list = document.FindElements(By.XPath("//td"));

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].TagName, Is.EqualTo("td"));
            Assert.That(list[0].Text, Is.EqualTo("foo"));
            Assert.That(list[1].TagName, Is.EqualTo("td"));
            Assert.That(list[1].Text, Is.EqualTo("bar"));
        }
    }
}