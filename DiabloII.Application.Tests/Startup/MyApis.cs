using DiabloII.Application.Tests.Suggestions;

namespace DiabloII.Application.Tests.Startup
{
    public class MyApis
    {
        public MyApis(MyHttpClient httpClient)
        {
            Suggestions = new SuggestionApi(httpClient);
        }

        public SuggestionApi Suggestions { get; set; }
    }
}