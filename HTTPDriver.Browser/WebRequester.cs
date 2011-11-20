using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace HTTPDriver.Browser
{
    public class WebRequester : IWebRequester
    {
        private HttpWebRequest _request;
        private bool _shouldFollowRedirects=true;


        public WebRequester AutomaticallyFollowRedirects(bool shouldFollowRedirects)
        {
            _shouldFollowRedirects = shouldFollowRedirects;
            return this;
        }
        
        public IWebResponder Get(string url)
        {
            Create(url);
            return new WebResponder(_request.GetResponse());
        }

        public IWebResponder Post(string url, IDictionary<string, string>  postdata)
        {
            Create(url);
            _request.Method = "post";
            _request.ContentType = "application/x-www-form-urlencoded";

            var postDataString = CreatePostDataString(postdata);
            _request.ContentLength = postDataString.Length;
            var stream = new StreamWriter(_request.GetRequestStream());
            stream.Write(postDataString);
            stream.Close();

            return new WebResponder(_request.GetResponse());
        }

        private void Create(string url)
        {
            _request = (HttpWebRequest) WebRequest.Create(url);
            _request.AllowAutoRedirect = _shouldFollowRedirects;
            _request.CookieContainer = new CookieContainer();
        }

        private string CreatePostDataString(IEnumerable<KeyValuePair<string, string>> postdata)
        {
            return string.Join("&", (from value in postdata
                    select value.Key + "=" + value.Value).ToArray());
        }
    }
}