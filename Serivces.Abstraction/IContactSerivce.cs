using System.Threading.Tasks;
using Sharded.DTO;
namespace Serivces.Abstraction;

public interface IContactSerivce
{
    Task<IEnumerable<ContactDTO>> Search(ContactRequestDTO request);
    Task Add(ContactRequestDTO request);
    Task Delete(int id);
}
