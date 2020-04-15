namespace DiabloII.Application.Tests.Apis
{
    public class ApiContext
    {
        public readonly SuggestionsApi Suggestions;
        
        public readonly ItemsApi Items;

        public readonly ErrorLogsApi ErrorLogs;

        public ApiContext(HttpContext httpContext)
        {
            Suggestions = new SuggestionsApi(httpContext);
            Items = new ItemsApi(httpContext);
            ErrorLogs = new ErrorLogsApi(httpContext);
        }
    }
}