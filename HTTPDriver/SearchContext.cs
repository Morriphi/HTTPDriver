using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace HTTPDriver
{
    public class SearchContext : ISearchContext
    {
        public IWebElement FindElement(By @by)
        {
            throw new System.NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            throw new System.NotImplementedException();
        }
    }
}