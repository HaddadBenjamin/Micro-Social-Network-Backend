namespace DiabloII.Items.Api.Responses.Suggestions
{
    public class SuggestionVoteDto
    {
        public string Ip { get; set; }

        public bool IsPositive { get; set; }
    }

    public class SuggestionCommentDto
    {
        public string Ip { get; set; }
        
        public string Comment { get; set; }
    }
}