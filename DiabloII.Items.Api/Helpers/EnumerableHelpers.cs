using System.Collections.Generic;
using System.Linq;

namespace DiabloII.Items.Api.Helpers
{
    public static class EnumerableHelpers
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> sequence) => sequence == null || !sequence.Any();
    }
}
