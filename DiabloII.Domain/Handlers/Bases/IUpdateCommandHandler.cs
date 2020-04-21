namespace DiabloII.Domain.Handlers.Bases
{
    public interface IUpdateCommandHandler<UpdateCommand, DataModel>
    {
        DataModel Update(UpdateCommand command);
    }
}