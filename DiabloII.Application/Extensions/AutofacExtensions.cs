using System;
using System.Linq;
using Autofac;

namespace DiabloII.Application.Extensions
{
    public static class AutofacExtensions
    {
        public static void RegisterAllImplementedInterfaceAndSelfFromAssemblies(this ContainerBuilder containerBuilder, params Type[] types)
        {
            var assemblies = types.Select(t => t.Assembly).ToArray();

            containerBuilder
                .RegisterAssemblyTypes(assemblies)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}