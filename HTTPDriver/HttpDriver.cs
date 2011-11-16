using System.Collections.ObjectModel;
using System.Net;
using HTTPDriver.Browser;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class HttpDriver : IWebDriver
    {
        private readonly BrowserEngine _engine;
        private readonly INavigation _navigation;

        public WebHeaderCollection Headers { get { return _engine.Headers; } }
        public HttpStatusCode StatusCode { get { return _engine.ResponseStatusCode; } }

        public HttpDriver(IWebRequester webRequester)
        {
            _engine = new BrowserEngine(webRequester);
            _navigation = new Navigation(this);
        }

        public IWebElement FindElement(By by)
        {
            return by.FindElement(new WebElementFinder(_engine.Page.HtmlNode(), Navigate()));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return by.FindElements(new WebElementFinder(_engine.Page.HtmlNode(), Navigate()));
        }

        public void Dispose()
        {
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
            get
            {
                return _engine.Page.Title();
            }
        }

        public string PageSource
        {
            get { return _engine.Page.Html(); }
        }

        public void SendRequest()
        {  
            _engine.Load(Url);
        }

        public string CurrentWindowHandle
        {
            get { throw new System.NotImplementedException(); }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}