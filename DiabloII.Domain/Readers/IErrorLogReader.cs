using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers
{
    public interface IErrorLogReader : IGetAllReader<ErrorLog>
    {
    }
}