using HTTPDriver.FunctionalTests.Helpers;
using NUnit.Framework;

namespace HTTPDriver.FunctionalTests
{
    [SetUpFixture]
    public class HttpTestSiteFixture
    {
        private readonly TestServer _server;

        public HttpTestSiteFixture()
        {
            _server = new TestServer(9001, new Site(@"..\..\..\HTTPDriver.TestSite", "/TestSite"));   
        }

        [SetUp]
        public void BeforeTests()
        {
            _server.Start();
        }

        [TearDown]
        public void AfterTests()
        {
            _server.Stop();
        }
    }
}