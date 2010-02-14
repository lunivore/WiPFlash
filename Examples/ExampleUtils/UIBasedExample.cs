using System.Diagnostics;
using NUnit.Framework;

namespace Examples.ExampleUtils
{
    public class UIBasedExample
    {
        protected const string EXAMPLE_APP_NAME = "ExampleUIs";

        protected const string EXAMPLE_APP_PATH =
            @"..\..\..\" + EXAMPLE_APP_NAME + @"\bin\debug\" + EXAMPLE_APP_NAME + ".exe";

        [TearDown]
        public void CloseAllExampleApplications()
        {
            Process[] processes = Process.GetProcessesByName(EXAMPLE_APP_NAME);
            foreach (var process in processes)
            {
                process.CloseMainWindow();
            }
        }
    }
}