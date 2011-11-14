using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace HTTPDriver
{
    public class WebResponder : IWebResponder
    {
        private readonly HtmlDocument _documentNode = new HtmlDocument();

        public WebResponder(WebResponse response)
        {
            if(response==null)
                throw new ArgumentNullException();
            
            var responseStream = response.GetResponseStream();

            if(responseStream==null)
                throw new ArgumentNullException();
            
            CreateDocumentNode(responseStream);
        }

        private void CreateDocumentNode(Stream responseStream)
        {
            _documentNode.Load(responseStream);
        }
        
        public string GetTitle()
        {
            return _documentNode.DocumentNode.SelectSingleNode("//title").InnerText;
        }

        public string GetPageSource()
        {
            using (var stringWriter = new StringWriter())
            {
                _documentNode.Save(stringWriter);
                return stringWriter.ToString();
            }
        }

        public HtmlNode GetDocumentElement()
        {
            return _documentNode.DocumentNode;
        }
    }
}