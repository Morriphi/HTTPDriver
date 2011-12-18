using System;
using HtmlAgilityPack;

namespace HTTPDriver.Browser
{
    public class Page
    {
        private readonly HtmlNode _html;
        public event Action<FormSubmission> FormSubmitted;

        public Page(HtmlNode html)
        {
            _html = html;
            FormSubmitted += delegate { };
        }

        public Page(string html)
        {
            HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("form");
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            _html = htmlDocument.DocumentNode.FirstChild;
        }

        public string Title()
        {
            var title = _html.SelectSingleNode("//title");
            return title != null ? title.Text() : "";
        }

        public string Html()
        {
            return _html.OuterHtml;
        }

        public string SelectSingleNodeText(string xpath)
        {
            return _html.SelectSingleNode(xpath).Text();
        }

        public HtmlNode HtmlNode()
        {
            return _html;
        }

        public Element FindById(string id)
        {
            var element = new Element(_html.OwnerDocument.GetElementbyId(id));
            element.Clicked += ElementClicked;
            return element;
        }

        private void ElementClicked(Element element)
        {
            // TODO: this logic probably shouldn't go here
            var form = element.HtmlNode.FindParent("form");
            if(form != null)
                FormSubmitted(new FormSubmission(form.Attr("action"), form.Attr("method")));
        }
    }

    public class FormSubmission : EventArgs
    {
        public FormSubmission(string action, string method)
        {
            Action = action;
            Method = method;
        }

        public string Action { get; private set; }

        public string Method { get; private set; }
    }
}