namespace DiabloII.Items.Api.DbContext.Suggestions
{
    public class SuggestionVote
    {
        public int Id { get; set; }

        public Suggestion Suggestion { get; set; }
        
        public int SuggestionId { get; set; }
        
        public bool IsPositive { get; set; }

        public string Ip { get; set; }
    }
}