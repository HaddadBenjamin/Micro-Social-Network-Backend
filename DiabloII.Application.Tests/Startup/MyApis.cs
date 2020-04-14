using DiabloII.Application.Tests.Steps.ErrorLogs;
using DiabloII.Application.Tests.Steps.Items;
using DiabloII.Application.Tests.Steps.Suggestions;

namespace DiabloII.Application.Tests.Startup
{
    public class MyApis
    {
        public readonly SuggestionsApi Suggestions;
        
        public readonly ItemsApi Items;

        public readonly ErrorLogsApi ErrorLogs;

        public MyApis(MyHttpClient httpClient)
        {
            Suggestions = new SuggestionsApi(httpClient);
            Items = new ItemsApi(httpClient);
            ErrorLogs = new ErrorLogsApi(httpClient);
        }
    }
}