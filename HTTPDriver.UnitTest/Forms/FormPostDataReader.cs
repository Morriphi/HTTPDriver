using System.Collections.Generic;
using System.Linq;
using HTTPDriver.Browser;
using HTTPDriver.Browser.UnitTest;
using HtmlAgilityPack;
using NUnit.Framework;

namespace HTTPDriver.UnitTest.Forms
{
    [TestFixture]
    public class FormPostDataReaderTests
    {
        [Test]
        public void ReadActionFromForm()
        {
            var formHtml = new HtmlNodeBuilder("<form method=\"post\" action=\"result.aspx\"></form>").Build();
            var reader = new FormPostDataReader(formHtml);

            Assert.That(reader.Action, Is.EqualTo("result.aspx"));
        }

        [Test]
        public void ReadMethodFromForm()
        {
            var formHtml = new HtmlNodeBuilder("<form method=\"post\" action=\"result.aspx\"></form>").Build();
            var reader = new FormPostDataReader(formHtml);

            Assert.That(reader.Method, Is.EqualTo("post"));
        }

        [Test]
        public void ReadPostDataFromInputFields()
        {
            var formHtml = new HtmlNodeBuilder("<form method=\"post\" action=\"result.aspx\">" +
                                               "<input id=\"login\" type=\"text\" value=\"helephant\" />" +
                                               "<input id=\"password\" type=\"password\" value=\"i love joe\" />" +
                                               "</form>").Build();
            var reader = new FormPostDataReader(formHtml);
            var postData = reader.PostData;

            Assert.That(postData["login"], Is.EqualTo("helephant"));
            Assert.That(postData["password"], Is.EqualTo("i love joe"));
        }

        [Test]
        public void ReadPostDataFromSelectFieldsWithOptionsWithValueAttribute()
        {
            var formHtml = new HtmlNodeBuilder("<form method=\"post\" action=\"result.aspx\">" +
                                               "<select id=\"salary\">" +
                                               "<option value=\"20000\">£20,000</option>" +
                                               "<option value=\"30000\">£30,000</option>" +
                                               "<option value=\"40000+\" selected>£40,000+</option>" +
                                               "</select>" +
                                               "</form>").Build();
            var reader = new FormPostDataReader(formHtml);
            var postData = reader.PostData;

            Assert.That(postData["salary"], Is.EqualTo("40000+"));
        }

        [Test]
        public void ReadPostDataFromSelectFieldsWithOptionsWithNoValueAttribute()
        {
            var formHtml = new HtmlNodeBuilder("<form method=\"post\" action=\"result.aspx\">" +
                                               "<select id=\"salary\">" +
                                               "<option>£20,000</option>" +
                                               "<option>£30,000</option>" +
                                               "<option selected>£40,000+</option>" +
                                               "</select>" +
                                               "</form>").Build();
            var reader = new FormPostDataReader(formHtml);
            var postData = reader.PostData;

            Assert.That(postData["salary"], Is.EqualTo("£40,000+"));
        }
    }

    public class FormPostDataReader
    {
        private readonly HtmlNode _formHtml;

        public FormPostDataReader(HtmlNode formHtml)
        {
            _formHtml = formHtml;
        }

        public string Action
        {
            get { return _formHtml.Attr("action") ?? ""; }
        }

        public string Method
        {
            get { return _formHtml.Attr("method") ?? ""; }
        }

        public IDictionary<string, string> PostData
        {
            get 
            { 
                var postData = new Dictionary<string, string>();

                var inputFields = _formHtml.SelectNodes("//input");
                if (inputFields != null)
                {
                    foreach (var inputField in inputFields)
                    {
                        var key = inputField.FieldName();
                        if (!string.IsNullOrEmpty(key))
                            postData.Add(key, inputField.Attr("value"));
                    }
                }

                var selectFields = _formHtml.SelectNodes("//select");
                if (selectFields != null)
                {
                    foreach (var selectField in selectFields)
                    {
                        var key = selectField.FieldName();
                        if (!string.IsNullOrEmpty(key))
                        {
                            var selectedOption = (from option in selectField.SelectNodes("//option").Cast<HtmlNode>()
                                                  where option.Attributes["selected"] != null
                                                  select option).FirstOrDefault();
                            var value = selectedOption.Attr("value") ?? selectedOption.Text();
                            postData.Add(key, value);
                        }
                    }
                }

                return postData;
            }
        }
    }
}
