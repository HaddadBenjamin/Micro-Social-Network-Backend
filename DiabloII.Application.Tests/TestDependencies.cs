using DiabloII.Application.Tests.Apis.Domains.ErrorLogs;
using DiabloII.Application.Tests.Apis.Domains.Items;
using DiabloII.Application.Tests.Apis.Domains.Notifications;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Users;
using DiabloII.Application.Tests.Contexts.Domains.Notifications;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Users;
using DiabloII.Application.Tests.Repositories.Suggestions;
using DiabloII.Application.Tests.Services.Http;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.Handlers;
using DiabloII.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace DiabloII.Application.Tests
{
    public static class TestDependencies
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var testContext = new TestContext();
            var services = testContext.Services;

            services
                .AddSingleton(testContext.DbContext)
                .AddSingleton<IHttpService>(testContext.HttpService)
                .AddSingleton<IUsersTestContext, UsersTestContext>()
                .AddSingleton<ISuggestionsTestContext, SuggestionsTestContext>()
                .AddSingleton<INotificationsTestContext, NotificationsTestContext>()
                .AddSingleton<ISuggestionsRepository, SuggestionsRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<INotificationRepository, NotificationRepository>()
                .AddSingleton<ISuggestionsApi, SuggestionsApi>()
                .AddSingleton<IItemsApi, ItemsApi>()
                .AddSingleton<IErrorLogsApi, ErrorLogsApi>()
                .AddSingleton<IUsersApi, UsersApi>()
                .AddSingleton<INotificationsApi, NotificationsApi>();

            return testContext.Services;
        }
    }
}