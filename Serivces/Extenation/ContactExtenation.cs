using System;
using Sharded.DTO;
using Domain.Entity;
namespace Serivces.Extenation;

public static class ContactExtenation
{
    public static ContactDTO ToDto(this Contact contact){
        if(contact==null)
            throw new ArgumentNullException(nameof(contact), "The contact cannot be null.");
        return new ContactDTO{Id=contact.id,Name=contact.name,Email=contact.email,PhoneNumber=contact.phonenumber};
    }

    public static Contact ToDomain(this ContactRequestDTO contactRequest){
        if(contactRequest==null)
            throw new ArgumentNullException(nameof(contactRequest), "The contact request cannot be null.");

        return new Contact{name=contactRequest.Name ?? "",email=contactRequest.Email?? "",phonenumber=contactRequest.PhoneNumber?? ""};
    }
}
