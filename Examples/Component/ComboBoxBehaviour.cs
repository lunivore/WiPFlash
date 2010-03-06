﻿#region

using System;
using System.Collections.Generic;
using System.Windows.Automation;
using Examples.ExampleUtils;
using NUnit.Framework;
using WiPFlash.Components;
using WiPFlash.Exceptions;

#endregion

namespace Examples.Component
{
    [TestFixture]
    public class NonEditableComboBoxBehaviour : ComboBoxBehaviour<ComboBox>
    {
        [Test]
        public void ShouldProvideCurrentItems()
        {
            ComboBox comboBox = CreateWrapper();
            var items = new List<string>(comboBox.Items);
            foreach (var list in items)
            {
                Console.WriteLine(list);
            }
            Assert.True(items.Contains("PetFood[Carnivorous]"));
            Assert.True(items.Contains("PetFood[Eats People]"));
            
        }

        protected override ComboBox CreateWrapperWith(AutomationElement element)
        {
            return new ComboBox(element);
        }

        protected override ComboBox CreateWrapper()
        {
            Window window = LaunchPetShopWindow();
            return window.Find<ComboBox>(ComboBoxName);
        }

        protected override string SelectableValue
        {
            get { return "PetFood[Carnivorous]"; }
        }

        protected override string ComboBoxName
        {
            get { return "petFoodInput"; }
        }
    }

    [TestFixture]
    public abstract class ComboBoxBehaviour<T> : AutomationElementWrapperExamples<T> where T : ComboBox
    {
        [Test]
        public void ShouldAllowValuesToBeSetUsingToStringWhenNotEditable()
        {
            var window = LaunchPetShopWindow();
            var nonEditableBox = window.Find<ComboBox>(ComboBoxName);
            nonEditableBox.Select("");
            Assert.AreEqual("", nonEditableBox.Selection);
            nonEditableBox.Select(SelectableValue);
            Assert.AreEqual(SelectableValue, nonEditableBox.Selection);
        }

        [Test]
        public void ShouldComplainIfValueSelectedWhichIsntInTheList()
        {
            var window = LaunchPetShopWindow();
            var nonEditableBox = window.Find<ComboBox>(ComboBoxName);
            try
            {
                nonEditableBox.Select("Wibble");
                Assert.Fail("Should have complained that it couldn't find the element Wibble");
            } 
            catch (FailureToFindException)
            {
                
            }
        }

        protected virtual string ComboBoxName { get{ return string.Empty; } }
        protected virtual string SelectableValue { get { return string.Empty; } }
    }
}
