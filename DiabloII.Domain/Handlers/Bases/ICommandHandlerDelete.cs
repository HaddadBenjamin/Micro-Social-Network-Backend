namespace DiabloII.Domain.Handlers.Bases
{
    public interface ICommandHandlerDelete<DeleteCommand, CommandHandlerResponse>
    {
        CommandHandlerResponse Delete(DeleteCommand command);
    }
}