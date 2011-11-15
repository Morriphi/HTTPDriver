using HtmlAgilityPack;

namespace HTTPDriver.Browser
{
    public interface IWebResponder
    {
        HtmlNode Page { get; }
    }
}