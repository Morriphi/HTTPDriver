using System;
using System.Net;
using HtmlAgilityPack;

namespace HTTPDriver.Browser
{
    public interface IWebResponder
    {
        HtmlNode Page { get; }
        HttpStatusCode StatusCode { get; }
        WebHeaderCollection Headers { get; }
        CookieCollection Cookies { get; }
        Uri Url { get; }
    }
}