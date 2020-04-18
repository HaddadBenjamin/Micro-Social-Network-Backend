namespace DiabloII.Domain.Models.Notifications
{
    public enum NotifierType
    {
        None = 0,
        InApp = 1,
        Mail = 1 << 1
    }
}