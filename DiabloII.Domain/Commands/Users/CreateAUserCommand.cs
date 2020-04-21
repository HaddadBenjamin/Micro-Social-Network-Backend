namespace DiabloII.Domain.Commands.Users
{
    public class CreateAUserCommand
    {
        public string UserId { get; set; }

        public string Email { get; set; }
    }
}