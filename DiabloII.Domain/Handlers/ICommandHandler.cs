namespace DiabloII.Domain.Handlers
{
    public interface ICommandHandler<Command> where Command : class
    {
        void Handle(Command command);
    }
}