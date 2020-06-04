using AutoMapper;
using DiabloII.Domain.Commands.Domains.ErrorLogs;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Infrastructure.DbContext;
using MediatR;

namespace DiabloII.Infrastructure.Handlers.Domains
{
    public class ErrorLogCommandHandler : RequestHandler<CreateErrorLogCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IMapper _mapper;

        public ErrorLogCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        protected override void Handle(CreateErrorLogCommand command)
        {
            var errorLog = _mapper.Map<ErrorLog>(command);

            _dbContext.ErrorLogs.Add(errorLog);
            _dbContext.SaveChanges();
        }
    }
}