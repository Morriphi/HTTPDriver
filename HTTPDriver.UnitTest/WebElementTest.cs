using System;
using HTTPDriver.Browser.UnitTest;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HTTPDriver.UnitTest
{
    public class WebElementTest
    {
        [Test] 
        public void Text()
        {
            var htmlNode = new HtmlNodeBuilder("<div>\r\nHello World        \r\n</div>").Build();

            var element = new WebElement(htmlNode, null);
            var textInAnElement = element.Text;

            Assert.That(textInAnElement, Is.EqualTo("Hello World"));
        }

        [Test]
        public void TagName()
        {
            var htmlNode = new HtmlNodeBuilder("<div>Hello DIV Tag</div>").Build();

            var element = new WebElement(htmlNode, null);

            Assert.That(element.TagName, Is.EqualTo("div"));
        }

        [Test]
        public void GetAttribute()
        {
            var htmlNode = new HtmlNodeBuilder("<a href=\"http://www.google.com\">Google</a>").Build();

            var element = new WebElement(htmlNode, null);

            Assert.That(element.GetAttribute("href"), Is.EqualTo("http://www.google.com"));
        }

        [Test]
        public void FindElement()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body><div id=\"page-body\">Hello world</div></body></html>").Build();

            var document = new WebElement(htmlNode, null);
            var div = document.FindElement(By.Id("page-body"));

            Assert.That(div.TagName, Is.EqualTo("div"));
            Assert.That(div.Text, Is.EqualTo("Hello world"));
        }

        [Test]
        public void FindElementShouldReturnNullIfNoElementsAreReturned()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                   "<a href=\"http://www.google.com\">google</a>" +
                   "</body></html>").Build();

            var document = new WebElementFinder(htmlNode,null);
            var paragraph = document.FindElement(By.TagName("p"));

            Assert.That(paragraph, Is.EqualTo(null));
        }

        [Test]
        public void FindElements()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                                           "<ul><li class=\"nav-item\">Item 1</li><li class=\"nav-item\">Item 2</li></ul>" +
                                           "</body></html>").Build();

            var document = new WebElement(htmlNode, null);
            var list = document.FindElements(By.ClassName("nav-item"));

            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list[0].TagName, Is.EqualTo("li"));
            Assert.That(list[1].TagName, Is.EqualTo("li"));
        }

        [Test]
        public void FindElementsShouldReturnAnEmptyListIfNoElementsAreReturned()
        {
            var htmlNode = new HtmlNodeBuilder("<html><body>" +
                   "<a href=\"http://www.google.com\">google</a>" +
                   "</body></html>").Build();

            var document = new WebElementFinder(htmlNode, null);
            var paragraphs = document.FindElements(By.TagName("p"));

            Assert.That(paragraphs, Is.Not.EqualTo(null));
            Assert.That(paragraphs.Count, Is.EqualTo(0));
        }

        [Test]
        public void ClickShouldFollowHrefs()
        {
            var nav = new Mock<INavigation>();

            var htmlNode = new HtmlNodeBuilder("<a href=\"http://www.google.com\">google</a>").Build();

            var webElement = new WebElement(htmlNode, nav.Object);
            webElement.Click();

            nav.Verify( x => x.GoToUrl(htmlNode.Attributes["href"].Value), Times.Once(), "Click() should cause INavigator to go to the url.");
        }

        [Test]
        public void ClickShouldBeIgnoredWhenNotApplicable()
        {
            var nav = new Mock<INavigation>();

            var htmlNode = new HtmlNodeBuilder("<div>This is not a link</div>").Build();

            var webElement = new WebElement(htmlNode, nav.Object);
            webElement.Click();
        
            nav.Verify(x => x.GoToUrl(It.IsAny<string>()), Times.Never(),
                "Click should not do anything when not applied to a link.");
        }
    }
}