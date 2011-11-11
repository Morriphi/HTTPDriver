using System.Net;

namespace HTTPDriver
{
    public class WebRequester : IWebRequester
    {
        private HttpWebRequest _request;
        

        public IWebResponder Request(string url)
        {

            _request = (HttpWebRequest)WebRequest.Create(url);
            return new WebResponder(_request.GetResponse());

        }
    }
}