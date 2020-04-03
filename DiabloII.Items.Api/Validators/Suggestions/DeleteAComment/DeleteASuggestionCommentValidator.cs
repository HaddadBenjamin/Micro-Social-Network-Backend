using DiabloII.Items.Api.Validators.Suggestions.Vote;
using FluentValidation;

namespace DiabloII.Items.Api.Validators.Suggestions.DeleteAComment
{
    public class DeleteASuggestionCommentValidator : AbstractValidator<DeleteASuggestionCommentValidatorContext>
    {
        public DeleteASuggestionCommentValidator()
        {
            RuleFor(context => context.Dto.Ip)
                .SuggestionIpShouldNotBeNullOrEmpty()
                .SuggestionIpShouldBeAnIpV4();

            RuleFor(context => context)
                .SuggestionAndCommentShouldExistsAndBeRelatedToTheUserIp();
        }
    }
}