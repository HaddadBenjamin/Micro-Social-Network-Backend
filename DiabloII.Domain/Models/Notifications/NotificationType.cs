namespace DiabloII.Domain.Models.Notifications
{
    public enum NotificationType
    {
        None = 0,
        Other = 1,
        PatchNotes = 1 << 1,
        CreatedSuggestion = 1 << 2,
        NewCommentOnYourSuggestion = 1 << 3
    }
}