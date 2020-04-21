using System.Threading.Tasks;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Application.Tests.Mappers;
using DiabloII.Application.Tests.Repositories;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Steps.Suggestions.Comment
{
    [Binding]
    [Scope(Tag = "suggestions")]
    public class CommentASuggestionSteps
    {
        private readonly ISuggestions _suggestions;
      
        private readonly ISuggestionsRepository _suggestionsRepository;
       
        private readonly ISuggestionsTestContext _suggestionsContext;

        public CommentASuggestionSteps(ISuggestions suggestions, ISuggestionsRepository suggestionsRepository, ISuggestionsTestContext suggestionsContext)
        {
            _suggestions = suggestions;
            _suggestionsRepository = suggestionsRepository;
            _suggestionsContext = suggestionsContext;
        }

        [Given(@"I comment the suggestion ""(.*)""")]
        [When(@"I comment the suggestion ""(.*)""")]
        public async Task WhenICommentTheSuggestion(string suggestionContent, Table table)
        {
            var suggestionId = await _suggestionsRepository.GetSuggestionId(suggestionContent);
            var dto = table.CreateInstance<CommentASuggestionDto>();

            dto.SuggestionId = suggestionId;

            _suggestionsContext.VotedResource = await _suggestions.Create(dto);
        }

        [Then(@"the commented suggestion should be")]
        public void ThenTheCommentedSuggestionShouldBe(Table table) =>
            table.ShouldBeEqualsTo(_suggestionsContext.CommentedResource, SuggestionsTableMapper.ToSuggestionDto);
    }
}