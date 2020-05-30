using System;
using System.Linq;
using System.Reflection;

namespace DiabloII.Domain.Handlers
{
    public interface IMediator
    {
        public void Send<Command>(Command command) where Command : class;
    }


    public class Mediator : IMediator
    {
        private readonly Type[] assemblyClassTypes;

        public Mediator() => assemblyClassTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass)
            .ToArray();

        public void Send<Command>(Command command) where Command : class
        {
            var handlerGenericInterfaceType = typeof(ICommandHandler<>).GetType();
            var handlerInterfaceType = handlerGenericInterfaceType.MakeGenericType(handlerGenericInterfaceType);
            var handlerImplementationTypes = assemblyClassTypes
                .Where(type => type.GetInterfaces().Contains(handlerInterfaceType))
                .ToList();

            if (!handlerImplementationTypes.Any())
                return;

            handlerImplementationTypes.ForEach(type =>
            {
                var handlerImplementationType = Activator.CreateInstance(type) as ICommandHandler<Command>;

                handlerImplementationType?.Handle(command);
            });
        }
    }

    // TODO :
    // - R&D pour voir et comprendre comment on fait pour renvoyer des choses sur la partie Write de CQRS.
    // - Injecter mon Mediateur en tant que Singleton et le déplacer dans la partie infrastructure ?
    // - Il faudra voir comment renvoyer des choses dans une architecture CQRS dans la partie des commandhandleru.
}