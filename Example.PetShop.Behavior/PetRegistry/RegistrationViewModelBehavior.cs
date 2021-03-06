﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.PetShop.Behavior.ExampleUtils;
using Example.PetShop.Domain;
using Example.PetShop.PetRegistry;
using Moq;
using NUnit.Framework;

namespace Example.PetShop.Behavior.PetRegistry
{
    [TestFixture]
    public class RegistrationViewModelBehavior
    {
        private Mock<ILookAfterPets> _petRepository;
        private Pet _goldie;

        [SetUp]
        public void CreatePetRepository()
        {
            _goldie = new Pet
                          {
                              Name = "Goldie",
                              FoodType = PetFood.ALL[3],
                              PriceInPence = 10000,
                              Rules = new List<Rule>{Rule.SELL_IN_PAIRS},
                              Sold = true,
                              Type = new PetType("Carp")
                          };

            _petRepository = new Mock<ILookAfterPets>();
            _petRepository.SetupGet(pr => pr.Pets).Returns(new List<Pet> {_goldie});
        }

        [Test]
        public void ShouldCopyTheDetailsButNotNameOfAnExistingPet()
        {
            // Given an existing pet
            var model = new RegistrationViewModel(_petRepository.Object, new StubEventAggregator());
            model.Name = "Nemo";

            // When I copy that pet
            model.CopiablePets[0].CopyCommand.Execute(_goldie);

            // Then it should copy all the details
            // but not the name
            Assert.AreEqual("100.00", model.Price);
            Assert.Contains(Rule.SELL_IN_PAIRS, model.Rules);
            Assert.AreEqual(_goldie.FoodType, model.FoodType);
            Assert.AreEqual(_goldie.Type, model.PetType);
            Assert.AreEqual("Nemo", model.Name);
        }
    }
}
