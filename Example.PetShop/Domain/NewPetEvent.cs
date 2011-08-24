using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Events;

namespace Example.PetShop.Domain
{
    public class NewPetEvent : CompositePresentationEvent<Pet>
    {
    }
}