using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class HttpDriver : IWebDriver
    {
        private readonly IWebRequester _webRequester;
        private IWebResponder _webResponder;

    
        public HttpDriver(IWebRequester webRequester)
        {
            _webRequester = webRequester;
        }

        public IWebElement FindElement(By by)
        {
            var document = _webResponder.GetDocumentElement();
            return by.FindElement(new WebElementFinder(document));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void Quit()
        {
            throw new System.NotImplementedException();
        }

        public IOptions Manage()
        {
            throw new System.NotImplementedException();
        }

        public INavigation Navigate()
        {
            return new Navigation(this);
        }

        public ITargetLocator SwitchTo()
        {
            throw new System.NotImplementedException();
        }

        public string Url { get; set; }

        public string Title
        {
            get { return _webResponder.GetTitle().Trim(); }
        }

        public string PageSource
        {
            get { return _webResponder.GetPageSource(); }
        }

        public string CurrentWindowHandle
        {
            get { throw new System.NotImplementedException(); }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { throw new System.NotImplementedException(); }
        }

        public void SendRequest()
        {
            _webResponder = _webRequester.Request(Url);
        }
    }
}