using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiabloII.Items.Api.DbContext.Suggestions
{
    public class SuggestionVote
    {
        public Guid Id { get; set; }

        public Suggestion Suggestion { get; set; }
        
        public Guid SuggestionId { get; set; }
        
        public bool IsPositive { get; set; }

        public string Ip { get; set; }
    }
}