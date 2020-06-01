using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers.Domains
{
    public interface IUserReader : IReaderGetAll<User>
    {
    }
}