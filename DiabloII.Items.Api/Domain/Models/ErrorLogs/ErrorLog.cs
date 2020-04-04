using System;

namespace DiabloII.Items.Api.Domain.Models.ErrorLogs
{
    public class ErrorLog
    {
        public Guid Id { get; set; }

        public DateTime CreationDateUtc { get; set; }
       
        public string Content { get; set; }
    }
}