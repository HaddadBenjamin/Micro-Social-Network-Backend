using System.Collections.Generic;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Queries.Domains.Notifications;
using DiabloII.Domain.Readers.Domains;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Infrastructure.Readers
{
    public class SuggestionReader : ISuggestionReader
    {
        private readonly ISuggestionRepository _repository;

        public SuggestionReader(ISuggestionRepository repository) => _repository = repository;

        public IReadOnlyCollection<Suggestion> GetAll() => _repository.GetAll();

        public Suggestion Get(GetSuggestionQuery query) => _repository.Get(query.Id);
    }
}