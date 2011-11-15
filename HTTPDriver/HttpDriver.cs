using System.Collections.ObjectModel;
using HTTPDriver.Browser;
using HtmlAgilityPack;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class HttpDriver : IWebDriver
    {
        private readonly IWebRequester _webRequester;
        private INavigation _navigation;
        private HtmlNode _page;

        public HttpDriver(IWebRequester webRequester)
        {
            _webRequester = webRequester;
            _navigation = new Navigation(this);
        }

        public IWebElement FindElement(By by)
        {
            return by.FindElement(new WebElementFinder(_page, Navigate()));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return by.FindElements(new WebElementFinder(_page, Navigate()));
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
                // TODO: we should wrap this up in a document object
                var title = _page.SelectSingleNode("//title");
                if(title != null)
                    return title.Text();
                return "";
            }
        }

        public string PageSource
        {
            get { return _page.OuterHtml; }
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
            _page = _webRequester.Get(Url).Page;
        }
    }
}