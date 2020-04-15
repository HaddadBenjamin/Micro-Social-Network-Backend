using System.Threading.Tasks;

namespace DiabloII.Application.Tests.Apis.ErrorLogs
{
    public interface IErrorLogsApi
    {
        Task GetAll();
    }
}