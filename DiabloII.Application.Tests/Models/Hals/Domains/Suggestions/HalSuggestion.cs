using System.Collections.Generic;
using System.Linq;
using DiabloII.Application.Tests.Models.Hals.Common;

namespace DiabloII.Application.Tests.Models.Hals.Domains.Suggestions
{
    public class HalSuggestionDto
    {
        public IEnumerable<HalLinks> Votes { get; set; } = Enumerable.Empty<HalLinks>();

        public IEnumerable<HalLinks> Comments { get; set; } = Enumerable.Empty<HalLinks>();

        public IDictionary<string, HalLink> _Links { get; set; } = new Dictionary<string, HalLink>();
    }
}