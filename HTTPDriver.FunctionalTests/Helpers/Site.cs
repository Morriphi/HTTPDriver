using System.IO;

namespace HTTPDriver.FunctionalTests.Helpers
{
    public class Site
    {
        private readonly string _physicalPath;
        private readonly string _virtualPath;

        public Site(string physicalPath, string virtualPath)
        {
            _physicalPath = physicalPath;
            _virtualPath = virtualPath;
        }

        public string PhysicalPath
        {
            get { return Path.GetFullPath(_physicalPath); }
        }

        public string VirtualPath
        {
            get { return _virtualPath; }
        }
    }
}
