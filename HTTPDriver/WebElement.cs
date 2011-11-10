using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace HTTPDriver
{
    public class WebElement : IWebElement, IFindsById, IFindsByClassName, IFindsByCssSelector, IFindsByXPath, IFindsByTagName
    {
        private readonly HtmlNode _element;

        public WebElement(HtmlNode element)
        {
            if (element.NodeType == HtmlNodeType.Document)
                _element = element.FirstChild;
            else
                _element = element;
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public void SendKeys(string text)
        {
            throw new System.NotImplementedException();
        }

        public void Submit()
        {
            throw new System.NotImplementedException();
        }

        public void Click()
        {
            throw new System.NotImplementedException();
        }

        public string GetAttribute(string attributeName)
        {
            throw new System.NotImplementedException();
        }

        public string GetCssValue(string propertyName)
        {
            throw new System.NotImplementedException();
        }

        public string TagName
        {
            get { return _element.Name; }
        }

        public string Text
        {
            get { return _element.InnerText; }
        }

        public bool Enabled
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool Selected
        {
            get { throw new System.NotImplementedException(); }
        }

        public Point Location
        {
            get { throw new System.NotImplementedException(); }
        }

        public Size Size
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool Displayed
        {
            get { throw new System.NotImplementedException(); }
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