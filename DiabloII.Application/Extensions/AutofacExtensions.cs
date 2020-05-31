using System;
using System.Linq;
using Autofac;

namespace DiabloII.Application.Extensions
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder RegisterAllImplementedInterfaceAndSelfFromAssemblies(this ContainerBuilder containerBuilder, params Type[] types) =>
            RegisterAllImplementedInterfaceAndSelfFromAssemblies(containerBuilder, null, types);

        public static ContainerBuilder RegisterAllImplementedInterfaceAndSelfFromAssemblies(this ContainerBuilder containerBuilder, Func<Type, bool> typeFilters = default, params Type[] types)
        {
            var assemblies = types.Select(t => t.Assembly).ToArray();
            var registrationBuilder = containerBuilder.RegisterAssemblyTypes(assemblies);

            if (typeFilters is null)
                registrationBuilder = registrationBuilder.Where(type => typeFilters is null ? true : typeFilters.Invoke(type));

            registrationBuilder
                .AsImplementedInterfaces()
                .AsSelf()
                .SingleInstance();

            return containerBuilder;
        }
    }
}