using System;
using System.Collections.Generic;
using System.Text;
using Sharded.DTOs;
using System.Threading.Tasks;

namespace Core.Services.Abststrations
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> Search(ContactRequestDTO contactRequest);
        void Add(ContactRequestDTO request);
        Task Delete(int id);
    }
}
