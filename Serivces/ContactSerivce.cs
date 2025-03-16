using Serivces.Abstraction;
using Sharded.DTO;
using Domain.Repositories;
using FluentValidation;
using Serivces.Extenation;
using Domain.Entity;
using System.Threading.Tasks;

namespace Serivces;

public class ContactSerivce:IContactSerivce
{
    private readonly IContactRepository _contactRepository;
    private readonly IValidator<ContactRequestDTO> _validator;
    public ContactSerivce(IContactRepository contactRepository,IValidator<ContactRequestDTO> validator){
        _contactRepository=contactRepository;
        _validator=validator;
    }

    public async Task<IEnumerable<ContactDTO>> Search(ContactRequestDTO request){
        var contacts=await _contactRepository.Get(request.ToDomain());
    
        var contactDTOs=contacts.Select(c=>c.ToDto());
        if(contactDTOs==null || contactDTOs.Count<ContactDTO>()==0)
            throw new NullReferenceException("Contact is not found");

        return await Task.FromResult(contactDTOs);
    }
    public async Task Add(ContactRequestDTO request){
        var validationResult=await _validator.ValidateAsync(request);
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        _contactRepository.Add(request.ToDomain());
    }
    public async Task Delete(int id){
        var contact=await _contactRepository.GetUserByidAsync(id);
        if(contact==null)
            throw new NullReferenceException("Contact is not found");

        _contactRepository.Delete(contact);
    }
}
