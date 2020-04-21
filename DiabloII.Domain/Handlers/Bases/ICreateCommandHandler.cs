namespace DiabloII.Domain.Handlers.Bases
{
    public interface ICreateCommandHandler<CreateCommand, DataModel>
    {
        DataModel Create(CreateCommand command);
    }
}