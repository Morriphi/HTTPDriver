using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest
{
    [TestFixture]
    public class PageTest
    {
        [Test]
        public void ParentFormShouldBeSubmittedWhenSubmitButtonIsClicked()
        {
            var page = new Page("<html><form id=\"save-form\" method=\"post\" action=\"/results\"><input type=\"submit\" id=\"save-button\" value=\"save\" /></form></html>");
            FormSubmission formSubmitted = null;
            page.FormSubmitted += evt => formSubmitted = evt;

            var saveButton = page.FindById("save-button");
            saveButton.Click();

            Assert.That(formSubmitted.Action, Is.EqualTo("/results"));
            Assert.That(formSubmitted.Method, Is.EqualTo("post"));
        }
    }
}
