using System;
using System.IO;

namespace HTTPDriver.FunctionalTests.Helpers
{
    public class TestServer
    {
        private const string Server = "WebDev.WebServer20.EXE";
        private const string ServerPath = @"..\..\..\lib\WebDevServer\64Bit\";

        private readonly CommandLine _commandLine;

        public TestServer(int port, Site site)
        {
            if (IsNot64Bit())
                throw new TestWebServerOnlyRunsOn64BitMachine();

            _commandLine = new CommandLine();

            _commandLine.BuildCommand(Server, ServerPath)
                .With("port", port)
                .With("path", site.PhysicalPath)
                .With("vpath", site.VirtualPath);
        }

        public void Start()
        {
            if (!File.Exists(ServerPath + Server))
                throw new FileNotFoundException("Unable to start server: Cannot find " + Server);

            _commandLine.Execute();
        }

        public void Stop()
        {
            _commandLine.Kill();
        }

        private static bool IsNot64Bit()
        {
            return IntPtr.Size != 8;
        }

        public class TestWebServerOnlyRunsOn64BitMachine : Exception { }
    }
}
