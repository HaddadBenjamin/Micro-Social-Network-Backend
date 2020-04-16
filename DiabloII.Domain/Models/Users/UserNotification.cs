using System;

namespace DiabloII.Domain.Models.Users
{
    public class UserNotification
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public bool NewPatchNotes { get; set; }

        public bool NewSuggestions { get; set; }

        public bool NewSuggestionComments { get; set; }

        public bool NewInformationRelativeToTheMod { get; set; }
    }
}