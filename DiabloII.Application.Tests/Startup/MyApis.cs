using DiabloII.Application.Tests.Suggestions;

namespace DiabloII.Application.Tests.Startup
{
    public class MyApis
    {
        public SuggestionApi Suggestions { get; set; }

        public MyApis(MyHttpClient httpClient) => Suggestions = new SuggestionApi(httpClient);
    }
}