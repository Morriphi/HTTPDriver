namespace HTTPDriver
{
    public interface IWebRequester
    {
        IWebResponder Request(string url);
    }
}