using DiabloII.Application.Tests.Steps.ErrorLogs;
using DiabloII.Application.Tests.Steps.Items;
using DiabloII.Application.Tests.Steps.Suggestions;

namespace DiabloII.Application.Tests.Contexts
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