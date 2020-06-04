using System.Collections.Generic;
using System.Linq;
using DiabloII.Application.Tests.Models.Hals.Common;

namespace DiabloII.Application.Tests.Models.Hals.Domains.Suggestions
{
    public class HalSuggestions
    {
        public IEnumerable<HalSuggestion> Elements { get; set; } = Enumerable.Empty<HalSuggestion>();

        public IDictionary<string, HalLink> _Links { get; set; } = new Dictionary<string, HalLink>();
    }
}
