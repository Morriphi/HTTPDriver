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
        public void GetAttribute()
        {
            var htmlNode = GetHtmlNodeFrom("<a href=\"http://www.google.com\">Google</a>");

            var element = new WebElement(htmlNode);

            Assert.That(element.GetAttribute("href"), Is.EqualTo("http://www.google.com"));
        }



    }
}