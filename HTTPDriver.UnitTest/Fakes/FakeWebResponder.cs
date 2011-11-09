namespace HTTPDriver.UnitTest.Fakes
{
    public class FakeWebResponder : IWebResponder
    {
        private readonly HtmlParser _htmlParser;

        public FakeWebResponder(HtmlParser htmlParser)
        {
            _htmlParser = htmlParser;
        }

        public string GetTitle()
        {
            return _htmlParser.GetTitle();
        }
    }
}
