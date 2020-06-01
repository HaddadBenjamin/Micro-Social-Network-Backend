using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Domain.Commands.ErrorLogs;
using DiabloII.Domain.Models.ErrorLogs;
using DiabloII.Infrastructure.DbContext;
using MediatR;

namespace DiabloII.Infrastructure.Handlers
{
    public class ErrorLogCommandHandler : IRequestHandler<CreateErrorLogCommand, Guid>
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IMapper _mapper;

        public ErrorLogCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateErrorLogCommand command, CancellationToken cancellationToken = default)
        {
            var errorLog = _mapper.Map<ErrorLog>(command);

            _dbContext.ErrorLogs.Add(errorLog);
            await _dbContext.SaveChangesAsync();

            return errorLog.Id;
        }
    }
}