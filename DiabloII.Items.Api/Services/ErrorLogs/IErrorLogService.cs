using DiabloII.Items.Api.DbContext.Suggestions;

namespace DiabloII.Items.Api
{
    public interface IErrorLogService
    {
        void Log(ErrorLog errorLog);
    }
}