namespace DiabloII.Domain.Handlers.Bases
{
    public interface IDeleteCommandHandler<DeleteCommand, CommandHandlerResponse>
    {
        CommandHandlerResponse Delete(DeleteCommand command);
    }
}