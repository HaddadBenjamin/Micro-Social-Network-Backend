using System;

namespace DiabloII.Items.Api.Application.Responses.ErrorLogs
{
    public class ErrorLogDto
    {
        public Guid Id { get; set; }

        public DateTime CreationDateUtc { get; set; }

        public string Content { get; set; }
    }
}
