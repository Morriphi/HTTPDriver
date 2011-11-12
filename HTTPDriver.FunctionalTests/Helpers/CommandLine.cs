using System.Diagnostics;

namespace HTTPDriver.FunctionalTests.Helpers
{
    public class CommandLine
    {
        private string _path;
        private string _command;
        private string _parameters;
        private Process _process;

        public ParameterListBuilder BuildCommand(string command, string path)
        {
            _command = command;
            _path = path;
            return new ParameterListBuilder(this);
        }

        public void Execute()
        {
            if(!ProcessRunning())
                _process = Process.Start(_path + _command, _parameters);
        }

        public void Kill()
        {
            if(ProcessRunning())
                _process.Kill();
        }

        public bool ProcessRunning()
        {
            return _process != null;
        }

        public class ParameterListBuilder
        {
            private readonly CommandLine _utility;

            public ParameterListBuilder(CommandLine utility)
            {
                _utility = utility;
            }

            public ParameterListBuilder With(string key, string value)
            {
                _utility._parameters += string.Format("/{0}:{1} ", key, value);
                return this;
            }

            public ParameterListBuilder With(string key, int value)
            {
                return With(key, value.ToString());
            }
        }
    }
}