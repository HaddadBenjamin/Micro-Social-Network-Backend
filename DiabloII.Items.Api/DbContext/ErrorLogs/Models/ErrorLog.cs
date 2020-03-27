using System;

namespace DiabloII.Items.Api.DbContext.ErrorLogs.Models
{
    public class ErrorLog
    {
        public Guid Id { get; set; }

        public DateTime CreationDateUtc { get; set; }
       
        public string Content { get; set; }
    }
}