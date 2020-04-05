using System.Collections.Generic;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Readers;
using DiabloII.Domain.Repositories;

namespace DiabloII.Infrastructure.Readers
{
    public class SuggestionReader : ISuggestionReader
    {
        private readonly ISuggestionRepository _repository;

        public SuggestionReader(ISuggestionRepository repository) => _repository = repository;

        public IReadOnlyCollection<Suggestion> GetAll() => _repository.GetAll();
    }
}