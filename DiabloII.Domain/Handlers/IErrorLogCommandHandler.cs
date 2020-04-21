using DiabloII.Domain.Models.ErrorLogs;

namespace DiabloII.Domain.Handlers
{
    public interface IErrorLogCommandHandler
    {
        void Create(ErrorLog errorLog);
    }
}