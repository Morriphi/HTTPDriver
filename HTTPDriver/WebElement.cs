using System.Collections.ObjectModel;
using System.Drawing;
using HtmlAgilityPack;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class WebElement : IWebElement
    {
        private readonly HtmlNode _element;

        public WebElement(HtmlNode element)
        {
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
            return _element.Attributes[attributeName].Value;
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
            return by.FindElement(new WebElementFinder(_element));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return by.FindElements(new WebElementFinder(_element));
        }
    }
}