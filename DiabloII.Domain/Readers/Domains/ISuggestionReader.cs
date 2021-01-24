using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Queries.Domains.Notifications;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers.Domains
{
    public interface ISuggestionReader :
        IReaderGetAll<Suggestion>,
        IReaderGet<Suggestion, GetSuggestionQuery>
    {
    }
}