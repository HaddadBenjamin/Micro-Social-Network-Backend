using DiabloII.Application.Tests.Apis.Domains.ErrorLogs;
using DiabloII.Application.Tests.Apis.Domains.Items;
using DiabloII.Application.Tests.Apis.Domains.Notifications;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Users;
using DiabloII.Application.Tests.Contexts.Domains.Notifications;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Users;
using DiabloII.Application.Tests.Repositories;
using DiabloII.Application.Tests.Services.Http;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.Repositories;
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

            services.AddSingleton(testContext.DbContext);
            services.AddSingleton<IHttpService>(testContext.HttpService);
            services.AddSingleton<IUsersTestContext, UsersTestContext>();
            services.AddSingleton<ISuggestionsTestContext, SuggestionsTestContext>();
            services.AddSingleton<INotificationsTestContext, NotificationsTestContext>();
            services.AddSingleton<ISuggestionsRepository, SuggestionsRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<INotificationRepository, NotificationRepository>();
            services.AddSingleton<ISuggestions, Suggestions>();
            services.AddSingleton<IItems, Items>();
            services.AddSingleton<IErrorLogsApi, ErrorLogsApi>();
            services.AddSingleton<IUsers, Users>();
            services.AddSingleton<INotifications, Notifications>();

            return testContext.Services;
        }
    }
}