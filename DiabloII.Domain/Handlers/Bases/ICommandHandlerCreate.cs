namespace DiabloII.Domain.Handlers.Bases
{
    public interface ICommandHandlerCreate<CreateCommand, DataModel>
    {
        DataModel Create(CreateCommand command);
    }
}