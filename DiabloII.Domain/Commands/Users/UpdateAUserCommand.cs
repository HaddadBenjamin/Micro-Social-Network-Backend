namespace DiabloII.Domain.Commands.Users
{
    public class UpdateAUserCommand
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public int AcceptedNotifications { get; set; }

        public int AcceptedNotifiers { get; set; }
    }
}