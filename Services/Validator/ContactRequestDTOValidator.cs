using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Sharded.DTOs;
using Core.Domain.Repositories;

namespace Core.Services.Validator
{
    public class ContactRequestDTOValidator:AbstractValidator<ContactRequestDTO>
    {
        private readonly IContactRepository contactRepository;

        public ContactRequestDTOValidator(IContactRepository contactRepository)
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(c=>c.Email).NotEmpty().EmailAddress().MustAsync(BeUniqueEmail).WithMessage("Email is required.");
            RuleFor(c=>c.PhoneNumber).NotEmpty().Matches(@"[0-9]").MinimumLength(11).MaximumLength(11).MustAsync(BeUniquePhone).WithMessage("PhoneNumber is required");
            this.contactRepository = contactRepository;
        }

        private async Task<bool> BeUniqueEmail(string email,CancellationToken token)
        {
            var contact=await contactRepository.GetUserByEmailAsync(email);
            return contact == null;
        }

        private async Task<bool> BeUniquePhone(string phonenumber, CancellationToken token)
        {
            var contact = await contactRepository.GetUserByphoneAsync(phonenumber);
            return contact == null;
        }
    }
}
