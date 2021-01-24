using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DiabloII.Infrastructure.Handlers.Bases
{
    public abstract class CommandHandler<TRequest> : IRequestHandler<TRequest>, IRequestHandler<TRequest, Unit>
        where TRequest : IRequest
    {
        Task<Unit> IRequestHandler<TRequest, Unit>.Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            this.Handle(request);
            return Unit.Task;
        }

        public abstract void Handle(TRequest request);
    }
}
