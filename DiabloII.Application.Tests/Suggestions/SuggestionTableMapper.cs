using System.Collections.Generic;
using System.Linq;
using DiabloII.Application.Responses.Suggestions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DiabloII.Application.Tests.Suggestions
{
    public static class SuggestionTableMapper
    {
        public static IReadOnlyCollection<SuggestionDto> ToSuggestionDtos(Table table) => table.Rows
            .Select(ToSuggestionDto)
            .ToList();

        public static SuggestionDto ToSuggestionDto(TableRow row) => new SuggestionDto
        {
            Id = row.GetGuid("Id"),
            Content = row.GetString("Content"),
            CreatedBy = row.GetString("CreatedBy"),
            PositiveVoteCount = row.GetInt32("PositiveVoteCount"),
            NegativeVoteCount = row.GetInt32("NegativeVoteCount"),
            Comments = row.GetString("Comments")
                ?.Split(';')
                .Select(comment =>
                {
                    var commentData = comment.Split(",");

                    return new SuggestionCommentDto
                    {
                        Comment = commentData[0],
                        CreatedBy = commentData[1]
                    };
                })
                .ToList()
        };

    }
}