#region

using System;
using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace WiPFlash.Examples.ExampleUtils
{
    public abstract class UIBasedExamples
    {
        protected Window _window;

        public const string EXAMPLE_APP_PATH =
            @"..\..\..\" + EXAMPLE_APP_NAME + @"\bin\debug\" + EXAMPLE_APP_NAME + ".exe";

        public const string EXAMPLE_APP_NAME = "Example.PetShop";
        public const string EXAMPLE_APP_WINDOW_NAME = "petShopWindow";

        [TearDown]
        public void CloseAllExampleApplications()
        {
            AutomationElementCollection windows = FindAllOpenWindows();
            foreach (AutomationElement window in windows)
            {
                ((WindowPattern)window.GetCurrentPattern(WindowPattern.Pattern)).Close();                
            }
            Thread.Sleep(200);
        }

        private AutomationElementCollection FindAllOpenWindows()
        {
            return AutomationElement.RootElement.FindAll(TreeScope.Children,
                                                         new PropertyCondition(
                                                             AutomationElement.
                                                                 AutomationIdProperty,
                                                             EXAMPLE_APP_WINDOW_NAME));
        }

        protected Window LaunchPetShopWindow()
        {
            Application application = new ApplicationLauncher(TimeSpan.Parse("00:00:20"))
                .Launch(EXAMPLE_APP_PATH, (s) => Assert.Fail("{0} - {1}", s, EXAMPLE_APP_PATH));
            _window = application.FindWindow(EXAMPLE_APP_WINDOW_NAME);
            _window.HandlerForFailingToFind = Assert.Fail;
            return _window;
        }
    }
}