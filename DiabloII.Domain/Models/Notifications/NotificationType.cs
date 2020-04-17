using System;

namespace DiabloII.Domain.Models.Notifications
{
    [Flags]
    public enum NotificationType
    {
        None = 0,
        All = 1,
        Other = 1 << 1,
        PatchNotes = 1 << 2,
        CreatedSuggestion = 1 << 3,
        CreatedComment = 1 << 4
    }
}