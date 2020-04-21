using DiabloII.Domain.Handlers;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Handlers
{
    public class ErrorLogCommandHandler : IErrorLogCommandHandler
    {
        private readonly ApplicationDbContext _dbContext;

        public ErrorLogCommandHandler(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public void Create(ErrorLog errorLog)
        {
            _dbContext.ErrorLogs.Add(errorLog);
            _dbContext.SaveChanges();
        }
    }
}