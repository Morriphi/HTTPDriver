using HtmlAgilityPack;

namespace HTTPDriver
{
    public interface IWebResponder
    {
        string GetTitle();
        string GetPageSource();
        HtmlNode GetDocumentElement();
    }
}