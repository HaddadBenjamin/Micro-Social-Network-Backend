using System.Collections.Generic;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Domain.Readers
{
    public interface ISuggestionReader
    {
        IReadOnlyCollection<Suggestion> GetAll();
    }
}