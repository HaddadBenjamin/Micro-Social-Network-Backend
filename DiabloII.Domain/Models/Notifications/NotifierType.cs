using System;

namespace DiabloII.Domain.Models.Notifications
{
    [Flags]
    public enum NotifierType
    {
        None = 0,
        InApp = 1,
        Mail = 1 << 1,
        Sms = 1 << 2,
    }
}