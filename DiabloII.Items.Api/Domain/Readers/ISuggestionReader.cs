using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.Suggestions;

namespace DiabloII.Items.Api.Domain.Readers
{
    public interface ISuggestionReader
    {
        IReadOnlyCollection<Suggestion> GetAll();
    }
}