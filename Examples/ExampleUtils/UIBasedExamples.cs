#region

using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Component;
using WiPFlash.Components;

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
                Console.WriteLine("Tearing down process {0{ at {1}", process.Id, DateTime.Now);
                process.WaitForInputIdle(Window.DEFAULT_TIMEOUT_IN_MILLIS);
                process.CloseMainWindow();
                process.WaitForExit(Window.DEFAULT_TIMEOUT_IN_MILLIS);
                Console.WriteLine("Process {0} torn down at {1}", process.Id, DateTime.Now);
            }
        }

        protected Window LaunchPetShopWindow()
        {
            Application application = new ApplicationLauncher().LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH);
            return application.FindWindow(EXAMPLE_APP_WINDOW_NAME);
        }
    }
}