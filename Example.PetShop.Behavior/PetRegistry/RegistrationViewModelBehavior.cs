using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                              Price = "100.00",
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
            var model = new RegistrationViewModel(_petRepository.Object);
            model.Name = "Nemo";
            model.CopyCommand.Execute(_goldie);

            Assert.AreEqual("100.00", model.Price);
            Assert.Contains(Rule.SELL_IN_PAIRS, model.Rules);
            Assert.AreEqual(_goldie.FoodType, model.FoodType);
            Assert.AreEqual(_goldie.Type, model.PetType);
            Assert.AreEqual("Nemo", model.Name);
        }
    }
}
