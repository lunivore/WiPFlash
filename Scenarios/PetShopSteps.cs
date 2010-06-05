using System;
using NUnit.Framework;
using WiPFlash;
using WiPFlash.Components;

namespace Scenarios
{
    public class PetShopSteps
    {
        public const string EXAMPLE_APP_PATH =
            @"..\..\..\" + EXAMPLE_APP_NAME + @"\bin\debug\" + EXAMPLE_APP_NAME + ".exe";

        public const string EXAMPLE_APP_NAME = "ExampleUIs";
        public const string EXAMPLE_APP_WINDOW_NAME = "petShopWindow";

        private readonly Universe _universe;

        public PetShopSteps(Universe universe)
        {
            _universe = universe;
        }

        public void IsRunning()
        {
            Application application = new ApplicationLauncher(TimeSpan.Parse("00:00:20")).LaunchOrRecycle(EXAMPLE_APP_NAME, EXAMPLE_APP_PATH, Assert.Fail);
            _universe.Window = application.FindWindow(EXAMPLE_APP_WINDOW_NAME);
        }
    }
}