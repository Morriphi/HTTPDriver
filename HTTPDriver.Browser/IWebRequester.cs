using System.Net;

namespace HTTPDriver.Browser
{
    public interface IWebRequester
    {
        IWebResponder Get(string url);
        void AddCookie(Cookie cookie);
    }
}