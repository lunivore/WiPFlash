#region

using System.Diagnostics;

#endregion

namespace WiPFlash.Component
{
    public class Application
    {
        private Process _process;

        public Application(Process process)
        {
            _process = process;
        }

        public Process Process
        {
            get { return _process; }
        }
    }
}
