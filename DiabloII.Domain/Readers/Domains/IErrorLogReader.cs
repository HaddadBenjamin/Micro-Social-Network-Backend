using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers.Domains
{
    public interface IErrorLogReader : IReaderGetAll<ErrorLog>
    {
    }
}