using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Entities;
using Core.Domain.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly PhoneBookContext phoneBookContext;

        public ContactRepository(PhoneBookContext phoneBookContext)
        {
            this.phoneBookContext = phoneBookContext;
        }

        public async Task<List<Contact>> Get(Contact contact)
        {
            var query= phoneBookContext.Contact.AsQueryable();

            if (!string.IsNullOrEmpty(contact.name))
            {
                query = query.Where(c => c.name.Contains(contact.name.Trim().ToLower()));
            }

            if (!string.IsNullOrEmpty(contact.email))
            {
                query = query.Where(c => c.email.Contains(contact.email.Trim().ToLower()));
            }

            if (!string.IsNullOrEmpty(contact.phonenumber))
            {
                query = query.Where(c => c.phonenumber.Contains(contact.phonenumber.Trim().ToLower()));
            }

            return await query.ToListAsync();
        }

        public async Task<Contact> GetUserByidAsync(int id)
        {
            return await phoneBookContext.Contact.FindAsync(id);
        }

        public async Task<Contact> GetUserByEmailAsync(string email)
        {
            return await phoneBookContext.Contact.FirstOrDefaultAsync(c=>c.email.Equals(email));
        }

        public async Task<Contact> GetUserByphoneAsync(string phonenumber)
        {
            return await phoneBookContext.Contact.FirstOrDefaultAsync(c => c.phonenumber.Equals(phonenumber));
        }

        public void Add(Contact contact)
        {
            phoneBookContext.Contact.Add(contact);
            phoneBookContext.SaveChanges();
        }

        public void Delete(Contact contact)
        {
            phoneBookContext.Contact.Remove(contact);
            phoneBookContext.SaveChanges();
        }
    }
}
