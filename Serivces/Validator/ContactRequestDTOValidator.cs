using Domain.Repositories;
using FluentValidation;
using Sharded.DTO;

namespace Serivces.Validator;

public class ContactRequestDTOValidator:AbstractValidator<ContactRequestDTO>
{
    private readonly IContactRepository _contactRepository;

    public ContactRequestDTOValidator(IContactRepository contactRepository){
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(c=>c.Email).NotEmpty().EmailAddress().MustAsync(BeUniqueEmail).WithMessage("Email is required.");
        RuleFor(c=>c.PhoneNumber).NotEmpty().Matches(@"[0-9]").MinimumLength(11).MaximumLength(11).MustAsync(BeUniquePhone).WithMessage("PhoneNumber is required");
        _contactRepository=contactRepository;
    }

    private async Task<bool> BeUniqueEmail(string email,CancellationToken token)
    {
        var contact=await _contactRepository.GetUserByEmailAsync(email);
        return contact == null;
    }

    private async Task<bool> BeUniquePhone(string phonenumber, CancellationToken token)
    {
        var contact = await _contactRepository.GetUserByphoneAsync(phonenumber);
        return contact == null;
    }
}
