using System;
using HtmlAgilityPack;

namespace HTTPDriver.Browser
{
    public static class HtmlNodeExtensions
    {
        public static HtmlNode FindParent(this HtmlNode element, string tagName)
        {
            var current = element;
            while (current != null && !current.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase))
                current = current.ParentNode;
            return current;
        }

        public static string Text(this HtmlNode element)
        {
            if(element.Name == "option" && element.NextSibling is HtmlTextNode)
            {
                // html agility pack can't read the text of <option> tags
                // http://stackoverflow.com/questions/293342/htmlagilitypack-drops-option-end-tags
                return Text(element.NextSibling);

            }
            return element.InnerText.Trim();
        }

        public static string Attr(this HtmlNode element, string attributeName)
        {
            return element.Attributes[attributeName] != null ? 
                element.Attributes[attributeName].Value : null;
        }

        public static string FieldName(this HtmlNode inputField)
        {
            return Attr(inputField, "id") ?? Attr(inputField, "name");
        }
    }
}
