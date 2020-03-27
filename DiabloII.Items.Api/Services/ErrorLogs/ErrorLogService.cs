using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions;

namespace DiabloII.Items.Api
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly ApplicationDbContext _dbContext;

        public ErrorLogService(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public void Log(ErrorLog errorLog)
        {
            _dbContext.ErrorLogs.Add(errorLog);
            _dbContext.SaveChanges();
        }
    }
}