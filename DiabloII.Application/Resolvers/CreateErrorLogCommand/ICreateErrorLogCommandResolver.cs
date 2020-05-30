using CreateErrorLog = DiabloII.Domain.Commands.ErrorLogs.CreateErrorLogCommand;

namespace DiabloII.Application.Resolvers.CreateErrorLogCommand
{
    public interface ICreateErrorLogCommandResolver : IResolver<CreateErrorLog>
    {
    }
}