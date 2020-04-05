using System.Collections.Generic;
using DiabloII.Items.Api.Domain.Models.Suggestions;
using DiabloII.Items.Api.Domain.Readers;
using DiabloII.Items.Api.Domain.Repositories;

namespace DiabloII.Items.Api.Infrastructure.Services
{
    public class SuggestionReader : ISuggestionReader
    {
        private readonly ISuggestionRepository _repository;

        public SuggestionReader(ISuggestionRepository repository) => _repository = repository;

        public IReadOnlyCollection<Suggestion> GetAll() => _repository.GetAll();
    }
}