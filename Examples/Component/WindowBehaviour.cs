﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using Examples.ExampleUtils;
using Moq;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Framework;
using ComboBox=WiPFlash.Components.ComboBox;

namespace Examples.Component
{
    [TestFixture]
    public class WindowBehaviour : UIBasedExamples
    {
        [Test]
        public void ShouldFindTheComponentUsingTheFinderWithItselfAsRoot()
        {
            var anElement = AutomationElement.RootElement;
            var textBox = new TextBox(anElement);
            var comboBox = new ComboBox(anElement);

            var finder = new Mock<IFindAutomationElements>();

            var window = new Window(anElement, finder.Object);

            finder.Setup(x => x.Find<TextBox>(window, "aTextInput")).Returns(textBox);
            finder.Setup(x => x.Find<ComboBox>(window, "aComboInput")).Returns(comboBox);

            Assert.AreEqual(textBox, window.Find<TextBox>("aTextInput"));
            Assert.AreEqual(comboBox, window.Find<ComboBox>("aComboInput"));
        }
    }
}