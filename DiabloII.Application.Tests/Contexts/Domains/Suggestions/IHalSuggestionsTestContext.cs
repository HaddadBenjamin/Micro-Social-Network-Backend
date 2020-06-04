using DiabloII.Application.Tests.Models.Hals.Domains.Suggestions;

namespace DiabloII.Application.Tests.Contexts.Domains.Suggestions
{
    public interface IHalSuggestionsTestContext
    {
        HalSuggestions HalResources { get; set; }
    }
}