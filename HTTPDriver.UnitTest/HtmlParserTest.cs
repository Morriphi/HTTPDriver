using NUnit.Framework;

namespace HTTPDriver.UnitTest
{
    public class HtmlParserTest
    {
        [Test]
        public void GetTitle()
        {
            var parser = new HtmlParser("<html><title>Test Title</title></html>");

            Assert.That(parser.GetTitle(), Is.EqualTo("Test Title"));

            parser = new HtmlParser("<html><title>Another Test Title</title></html>");

            Assert.That(parser.GetTitle(), Is.EqualTo("Another Test Title"));

            parser = new HtmlParser("<html><h1>Another Test Title</h1></html>");

            Assert.That(parser.GetTitle(), Is.EqualTo(""));
        }
    }
}
