﻿using DiabloII.Application.Tests.Apis.ErrorLogs;
using DiabloII.Application.Tests.Apis.Items;
using DiabloII.Application.Tests.Apis.Notifications;
using DiabloII.Application.Tests.Apis.Suggestions;
using DiabloII.Application.Tests.Apis.Users;
using DiabloII.Application.Tests.Contexts.Notifications;
using DiabloII.Application.Tests.Contexts.Suggestions;
using DiabloII.Application.Tests.Contexts.Users;
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
            services.AddSingleton<ISuggestionsApi, SuggestionsApi>();
            services.AddSingleton<IItemsApi, ItemsApi>();
            services.AddSingleton<IErrorLogsApi, ErrorLogsApi>();
            services.AddSingleton<IUsersApi, UsersApi>();
            services.AddSingleton<INotificationsApi, NotificationsApi>();

            return testContext.Services;
        }
    }
}