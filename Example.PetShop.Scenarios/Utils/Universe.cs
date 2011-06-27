#region

using System;
using Example.PetShop.Scenarios.Steps;
using WiPFlash.Components;

#endregion

namespace Example.PetShop.Scenarios.Utils
{
    public class Universe
    {
        public Window Window
        {
            get; set;
        }

        public MessageBoxSteps MessageBoxSteps
        {
            get; set;
        }
    }
}