#region

using Example.PetShop.Scenarios.Utils;
using NUnit.Framework;
using WiPFlash.Components;

#endregion

namespace Example.PetShop.Scenarios.Steps
{
    public class HistorySteps
    {
        private readonly Universe _universe;

        public HistorySteps(Universe universe)
        {
            _universe = universe;
        }

        public void ShouldContain(double price, params string[] expectedDetails)
        {
            _universe.Window.Find<Menu>("mainMenu").Find<Menu>("tabMenu").Select("TabPresenter[History]");
            var historyInput = _universe.Window.Find<RichTextBox>("historyInput");
            historyInput.WaitFor((hi, e) => ((RichTextBox)hi).Text.Contains(price.ToString("0.00")),
                src => Assert.Fail("History should have contained {0}"));    
            foreach (var detail in expectedDetails)
            {
                string valueForLocalModifiedClosure = detail;
                historyInput.WaitFor((hi, e) => ((RichTextBox)hi).Text.Contains(valueForLocalModifiedClosure),
                    src => Assert.Fail("History should have contained {0}"));
            }
        }

        public void ShouldIncludeMostRecentPet(string name)
        {
            var view = _universe.Window.Find<GridView>("lastPetsOutput");
            Assert.AreEqual(view.TextAt(0, 2), name);
        }
    }
}