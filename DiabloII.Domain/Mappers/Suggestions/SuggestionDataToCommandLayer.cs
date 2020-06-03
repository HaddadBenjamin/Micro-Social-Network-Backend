using System;
using AutoMapper;
using DiabloII.Domain.Commands.Domains.Notifications;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Domain.Mappers.Suggestions
{
    public class SuggestionDataToCommandLayer : Profile
    {
        public SuggestionDataToCommandLayer()
        {
            CreateMap<Suggestion, CreateANotificationCommand>().AfterMap((dataModel, command) =>
            {
                command.Type = NotificationType.CreatedSuggestion;
                command.Title = "A new suggestion has been created";
            });

            CreateMap<SuggestionComment, CreateANotificationCommand>().AfterMap((dataModel, command) =>
            {
                command.Type = NotificationType.NewCommentOnYourSuggestion;
                command.Title = "Someone commented your suggestion";
                command.Content = $"Suggestion : {dataModel.Suggestion.Content}{Environment.NewLine}Comment : {dataModel.Comment}";
            });
        }
    }
}