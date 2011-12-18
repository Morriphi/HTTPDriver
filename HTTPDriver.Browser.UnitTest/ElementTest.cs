using NUnit.Framework;

namespace HTTPDriver.Browser.UnitTest
{
    [TestFixture]
    public class ElementTest
    {
        [Test]
        public void ElementShouldRaiseEventWhenClickedIfClickable()
        {
            var buttonWasClicked = false;
            var page = new Page("<html><input type=\"submit\" id=\"save-button\" value=\"save\"></html>");

            var saveButton = page.FindById("save-button");
            saveButton.Clicked += obj => buttonWasClicked = true;
            saveButton.Click();

            Assert.That(buttonWasClicked, Is.True);
        }
    }
}
