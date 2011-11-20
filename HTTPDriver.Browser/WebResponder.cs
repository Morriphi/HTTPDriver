using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace HTTPDriver.Browser
{
    public class WebResponder : IWebResponder
    {
        private readonly HtmlDocument _documentNode = new HtmlDocument();
        private readonly HttpWebResponse _httpWebResponse;

        public WebResponder(WebResponse response)
        {
            if (response == null)
                throw new ArgumentNullException();

            var httpWebResponse = response as HttpWebResponse;

            if (httpWebResponse == null)
                throw new NotAnHttpWebResponseException();

            _httpWebResponse = httpWebResponse;
            
            var responseStream = httpWebResponse.GetResponseStream();

            if(responseStream==null)
                throw new ArgumentNullException();
            
            CreateDocumentNode(responseStream);
        }

        public HttpStatusCode StatusCode 
        { 
            get { return _httpWebResponse.StatusCode; }
        }

        public WebHeaderCollection Headers
        {
            get { return _httpWebResponse.Headers; }
        }

        public CookieCollection Cookies
        {
            get { return _httpWebResponse.Cookies; }
        }

        public Uri Url
        {
            get { return _httpWebResponse.ResponseUri; }
        }

        private void CreateDocumentNode(Stream responseStream)
        {
            _documentNode.Load(responseStream);
        }
        
        public string GetTitle()
        {
            return _documentNode.DocumentNode.SelectSingleNode("//title").InnerText;
        }

        public string PageSource
        {
            get
            {
                using (var stringWriter = new StringWriter())
                {
                    _documentNode.Save(stringWriter);
                    return stringWriter.ToString();
                }
            }
        }

        public HtmlNode Page
        {
            get { return _documentNode.DocumentNode; }
        }
    }

    public class NotAnHttpWebResponseException : Exception
    {
    }
}