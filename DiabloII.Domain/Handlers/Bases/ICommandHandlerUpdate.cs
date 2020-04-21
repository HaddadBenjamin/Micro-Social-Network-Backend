namespace DiabloII.Domain.Handlers.Bases
{
    public interface ICommandHandlerUpdate<UpdateCommand, DataModel>
    {
        DataModel Update(UpdateCommand command);
    }
}