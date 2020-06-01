using System.Collections.Generic;

namespace DiabloII.Application.Tests.Models.Hals.Common
{
    public class HalLinks
    {
        public IDictionary<string, HalLink> _Links { get; set; } = new Dictionary<string, HalLink>();
    }
}
