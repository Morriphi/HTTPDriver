using System.Collections.ObjectModel;
using System.Drawing;
using HtmlAgilityPack;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class WebElement : IWebElement
    {
        private readonly HtmlNode _element;
        private readonly INavigation _navigation;

        public WebElement(HtmlNode element, INavigation navigation)
        {
            _element = element;
            _navigation = navigation;
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
            HtmlAttribute htmlAttribute = _element.Attributes["href"];
            if (htmlAttribute == null)
                return;

            _navigation.GoToUrl(htmlAttribute.Value);
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
            get { return _element.InnerText.Trim(); }
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
            return by.FindElement(new WebElementFinder(_element, _navigation));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return by.FindElements(new WebElementFinder(_element, _navigation));
        }
    }
}