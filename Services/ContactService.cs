using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.Abststrations;
using Sharded.DTOs;
using Core.Domain.Repositories;
using FluentValidation;
using Core.Domain.Entities;

namespace Core.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly IValidator<ContactRequestDTO> validator;

        public ContactService(IContactRepository contactRepository,IValidator<ContactRequestDTO> validator)
        {
            this.contactRepository = contactRepository;
            this.validator = validator;
        }

        public async Task<IEnumerable<ContactDTO>> Search(ContactRequestDTO contactRequest)
        {
            var contacts= await contactRepository.Get(new Contact {name=contactRequest.Name,email=contactRequest.Email,phonenumber=contactRequest.PhoneNumber});

            if (contacts == null || contacts.Count == 0)
                throw new NullReferenceException("Contact is not found");

            return contacts.Select(c => new ContactDTO { Id = c.id, Name = c.name, Email = c.email, PhoneNumber = c.phonenumber });
        }

        public void Add(ContactRequestDTO request)
        {
            var validationResult=validator.Validate(request);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            contactRepository.Add(new Contact {name=request.Name,email=request.Email,phonenumber=request.PhoneNumber});
        }

        public async Task Delete(int id)
        {
            var contact= await contactRepository.GetUserByidAsync(id);
            if (contact == null)
                throw new ValidationException("Contact not existed before");

            contactRepository.Delete(contact);
        }
    }
}
