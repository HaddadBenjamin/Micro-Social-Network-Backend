using System.Collections.Generic;
using System.Linq;
using DiabloII.Application.Tests.Models.Hals.Common;

namespace DiabloII.Application.Tests.Models.Hals.Domains.Suggestions
{
    public class HalSuggestionsDto
    {
        public IEnumerable<HalSuggestionDto> Elements { get; set; } = Enumerable.Empty<HalSuggestionDto>();

        public IDictionary<string, HalLink> _Links { get; set; } = new Dictionary<string, HalLink>();
    }
}
