using System;
using Microsoft.Extensions.Logging;

namespace DiabloII.Items.Api.DbContext.Suggestions
{
    public class ApplicationLog
    {
        public Guid Id { get; set; }

        public DateTime CreationDateUtc { get; set; }
       
        public LogLevel Level { get; set; }

        public string Message { get; set; }
    }
}