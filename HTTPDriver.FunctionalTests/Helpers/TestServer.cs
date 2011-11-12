using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;

namespace HTTPDriver.FunctionalTests.Helpers
{
    public class TestServer
    {
        private const string Server = "WebDev.WebServer20.EXE";
        private const string ServerPath64Bit = @"..\..\..\lib\WebDevServer\64Bit\" + Server;

        private readonly int _port;
        private readonly Site _site;

        public TestServer(int port, Site site)
        {
            if (IsNot64Bit())
                throw new TestWebServerOnlyRunsOn64BitMachine();

            _port = port;
            _site = site;
        }

        public void Start()
        {
            if (!File.Exists(ServerPath64Bit))
                throw new FileNotFoundException("Unable to start server: Cannot find " + Server);

            if (ServerNotRunning())
                StartServer();
        }

        public void Stop()
        {
            foreach (var p in Process.GetProcessesByName(Server.Replace(".EXE", "")))
                p.Kill();
        }

        public Uri NormalizeUri(string pathAndQuery)
        {
            var vdir = string.Format(CultureInfo.InvariantCulture, "/{0}/", _site.VirtualPath.Trim('/')).Replace("//", "/");
            return
                new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}{1}{2}", _port, vdir, pathAndQuery));
        }

        private bool ServerNotRunning()
        {
            return GetCommandLinesProcesses().All(c => !IsServerCommand(c));
        }

        private void StartServer()
        {
            Process.Start(ServerPath64Bit, ServerCommandLine());
        }

        private string ServerCommandLine()
        {
            return string.Format(CultureInfo.InvariantCulture, "/port:{0} /path:\"{1}\" /vpath:\"{2}\"",
                                 _port, _site.PhysicalPath, _site.VirtualPath);
        }

        private bool IsServerCommand(string cmdLine)
        {
            return cmdLine.EndsWith(ServerCommandLine(), StringComparison.OrdinalIgnoreCase);
        }

        private static IEnumerable<string> GetCommandLinesProcesses()
        {
            var results = new List<string>();
            var wmiQuery = string.Format(CultureInfo.InvariantCulture,
                                            "select CommandLine from Win32_Process where Name='{0}'", Server);

            using (var searcher = new ManagementObjectSearcher(wmiQuery))
            {
                using (var retObjectCollection = searcher.Get())
                {
                    foreach (var retObject in retObjectCollection)
                        results.Add((string)retObject["CommandLine"]);
                }
            }
            return results;
        }

        private static bool IsNot64Bit()
        {
            return IntPtr.Size != 8;
        }

        public class TestWebServerOnlyRunsOn64BitMachine : Exception { }
    }
}
