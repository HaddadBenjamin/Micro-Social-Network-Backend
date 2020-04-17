using System;

namespace DiabloII.Domain.Models.Notifications
{
    [Flags]
    public enum NotifierType
    {
        None = 0,
        All = 1,
        InApp = 1 << 1,
        Mail = 1 << 2,
        Sms = 1 << 3,
    }
}