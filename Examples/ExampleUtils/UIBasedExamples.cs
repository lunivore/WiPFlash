#region

using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

#endregion

namespace Examples.ExampleUtils
{
    public abstract class UIBasedExamples
    {
        public const string EXAMPLE_APP_PATH =
            @"..\..\..\" + EXAMPLE_APP_NAME + @"\bin\debug\" + EXAMPLE_APP_NAME + ".exe";

        public const string EXAMPLE_APP_NAME = "ExampleUIs";
        public const string EXAMPLE_APP_WINDOW_NAME = "petShopWindow";

        [TearDown]
        public void CloseAllExampleApplications()
        {
            Process[] processes = Process.GetProcessesByName(EXAMPLE_APP_NAME);
            foreach (var process in processes)
            {
                process.CloseMainWindow();
            }
            Thread.Sleep(500);
        }
    }
}