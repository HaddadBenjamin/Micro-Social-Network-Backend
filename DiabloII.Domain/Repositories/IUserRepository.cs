namespace DiabloII.Domain.Repositories
{
    public interface IUserRepository
    {
        bool DoesUserExists(string userId);
    }
}