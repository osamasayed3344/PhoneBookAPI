using Domain.Repositories;
using Infrastruture.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
namespace Infrastruture.Repositories;

public class ContactRepositoriy:IContactRepository
{
    private readonly PhoneBookContext _phoneBookContext;
    public ContactRepositoriy(PhoneBookContext phoneBookContext){
        _phoneBookContext=phoneBookContext;
    }

    public async Task<List<Contact>> Get(Contact contact){
        var query=_phoneBookContext.Contacts.AsQueryable();

        if(!string.IsNullOrEmpty(contact.name)){
            query=query.Where(c=>c.name.Contains(contact.name.Trim().ToLower()));
        }

        if(!string.IsNullOrEmpty(contact.email)){
            query=query.Where(c=>c.email.Contains(contact.email.Trim().ToLower()));
        }

        if(!string.IsNullOrEmpty(contact.phonenumber)){
            query=query.Where(c=>c.phonenumber.Contains(contact.phonenumber.Trim().ToLower()));
        }

        return await query.ToListAsync();
    }

    public async Task<Contact?> GetUserByidAsync(int id){
        var contact=await _phoneBookContext.Contacts.FindAsync(id);
        return contact ?? null;
    }

    public async Task<Contact?> GetUserByEmailAsync(string email){
        return await _phoneBookContext.Contacts.FirstOrDefaultAsync(c=> c.email.Equals(email)) ?? null;
    }

    public async Task<Contact?> GetUserByphoneAsync(string phonenumber){
        return await _phoneBookContext.Contacts.FirstOrDefaultAsync(c=>c.phonenumber.Equals(phonenumber)) ?? null;
    }

    public void Add(Contact contact){
        _phoneBookContext.Add(contact);
        _phoneBookContext.SaveChanges();
    }
    public void Delete(Contact contact){
        _phoneBookContext.Contacts.Remove(contact);
        _phoneBookContext.SaveChanges();
    }
}
