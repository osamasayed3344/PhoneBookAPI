using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Entities;
using System.Threading.Tasks;

namespace Core.Domain.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> Get(Contact contact);
        Task<Contact> GetUserByidAsync(int id);
        Task<Contact> GetUserByEmailAsync(string email);
        Task<Contact> GetUserByphoneAsync(string phonenumber);
        void Add(Contact contact);
        void Delete(Contact contact);
    }
}
