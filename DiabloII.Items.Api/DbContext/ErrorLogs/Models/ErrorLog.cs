using System;
namespace DiabloII.Items.Api.DbContext.Suggestions
{
    public class ErrorLog
    {
        public Guid Id { get; set; }

        public DateTime CreationDateUtc { get; set; }
       
        public string Content { get; set; }
    }
}