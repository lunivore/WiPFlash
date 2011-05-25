using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.PetShop.Scenarios.Utils;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;

namespace Example.PetShop.Scenarios.Steps
{
    public class MessageBoxSteps
    {
        private readonly Universe _universe;
        private Window _messageWindow;

        public MessageBoxSteps(Universe universe)
        {
            _universe = universe;
        }

        public void ShouldAskUs(string message)
        {
            var messageLabel = MessageWindow.Find<Label>(FindBy.UiAutomationId("messageOutput"));
            Assert.AreEqual(message, messageLabel.Text);
        }

        protected Window MessageWindow
        {
            get
            {
                if (_messageWindow == null)
                {
                    _messageWindow = new WindowFinder().FindWindow(FindBy.WpfTitle("Please confirm"),
                                                                   TimeSpan.Parse("00:00:10"), Assert.Fail);
                }
                return _messageWindow;
            }
        }

        public void IsDeclined()
        {
            MessageWindow.Find<Button>(FindBy.UiAutomationId("cancelButton")).Click();
            _messageWindow = null;
        }

        public void IsConfirmed()
        {
            MessageWindow.Find<Button>(FindBy.UiAutomationId("confirmButton")).Click();
            _messageWindow = null;
        }
    }
}
