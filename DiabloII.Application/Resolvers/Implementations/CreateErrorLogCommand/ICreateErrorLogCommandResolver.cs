using DiabloII.Application.Resolvers.Bases;
using CreateErrorLog = DiabloII.Domain.Commands.ErrorLogs.CreateErrorLogCommand;

namespace DiabloII.Application.Resolvers.Implementations.CreateErrorLogCommand
{
    public interface ICreateErrorLogCommandResolver : IResolver<CreateErrorLog>
    {
    }
}