using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace HTTPDriver
{
    public class WebElementFinder : IFindsById, IFindsByClassName, IFindsByTagName, IFindsByCssSelector, IFindsByXPath, ISearchContext
    {
        private readonly HtmlNode _element;

        public WebElementFinder(HtmlNode element)
        {
            this._element = element;
        }

        public IWebElement FindElement(By @by)
        {
            return by.FindElement(this);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return by.FindElements(this);
        }

        public IWebElement FindElementById(string id)
        {
            return FindElementByCssSelector("#" + id);
        }

        public ReadOnlyCollection<IWebElement> FindElementsById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IWebElement FindElementByClassName(string className)
        {
            return FindElementByCssSelector("." + className);
        }

        public ReadOnlyCollection<IWebElement> FindElementsByClassName(string className)
        {
            return FindElementsByCssSelector("." + className);
        }

        public IWebElement FindElementByTagName(string tagName)
        {
            return FindElementsByTagName(tagName).FirstOrDefault();
        }

        public ReadOnlyCollection<IWebElement> FindElementsByTagName(string tagName)
        {
            return FindElementsByCssSelector(tagName);
        }

        public IWebElement FindElementByCssSelector(string cssSelector)
        {
            return FindElementsByCssSelector(cssSelector).FirstOrDefault();
        }

        public ReadOnlyCollection<IWebElement> FindElementsByCssSelector(string cssSelector)
        {
            var matches = _element.QuerySelectorAll(cssSelector);
            return HtmlNodesToWebElements(matches);
        }

        public IWebElement FindElementByXPath(string xpath)
        {
            return FindElementsByXPath(xpath).FirstOrDefault();
        }

        public ReadOnlyCollection<IWebElement> FindElementsByXPath(string xpath)
        {
            var matches = _element.SelectNodes(xpath);
            return HtmlNodesToWebElements(matches);
        }

        private ReadOnlyCollection<IWebElement> HtmlNodesToWebElements(IEnumerable nodes)
        {
            var webElements = (from match in nodes.Cast<HtmlNode>()
                               select new WebElement(match) as IWebElement).ToList();
            return new ReadOnlyCollection<IWebElement>(webElements);
        }
    }
}
