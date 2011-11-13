using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class HttpDriver : IWebDriver
    {
        private readonly IWebRequester _webRequester;
        private IWebResponder _webResponder;
        private INavigation _navigation;
        
        public HttpDriver(IWebRequester webRequester)
        {
            _webRequester = webRequester;
            _navigation = new Navigation(this);
        }

        public IWebElement FindElement(By by)
        {
            var document = _webResponder.GetDocumentElement();
            return by.FindElement(new WebElementFinder(document, Navigate()));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            var document = _webResponder.GetDocumentElement();
            return by.FindElements(new WebElementFinder(document, Navigate()));
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
            return _navigation;
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