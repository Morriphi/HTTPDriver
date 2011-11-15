namespace HTTPDriver.Browser
{
    public interface IWebRequester
    {
        IWebResponder Get(string url);
    }
}