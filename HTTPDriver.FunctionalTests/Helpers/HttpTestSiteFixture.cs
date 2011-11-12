using NUnit.Framework;

namespace HTTPDriver.FunctionalTests.Helpers
{
    [TestFixture]
    public class HttpTestSiteFixture
    {
        private readonly TestServer _server;

        public HttpTestSiteFixture()
        {
            _server = new TestServer(9001, new Site(@"..\..\..\HTTPDriver.TestSite", "/TestSite"));   
        }

        [TestFixtureSetUp]
        public void BeforeTests()
        {
            _server.Start();
        }

        [TestFixtureTearDown]
        public void AfterTests()
        {
            _server.Stop();
        }
    }
}