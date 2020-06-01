using System;

namespace DiabloII.Application.Responses.Read.ErrorLogs
{
    public class ErrorLogDto
    {
        public Guid Id { get; set; }

        public DateTime CreationDateUtc { get; set; }

        public string Content { get; set; }
    }
}
