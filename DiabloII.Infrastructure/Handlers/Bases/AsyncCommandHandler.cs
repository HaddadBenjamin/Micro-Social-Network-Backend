using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DiabloII.Infrastructure.Handlers.Bases
{
    public abstract class AsyncCommandHandler<TRequest> : AsyncRequestHandler<TRequest>, IRequestHandler<TRequest, Unit>
        where TRequest : IRequest
    {
        async Task<Unit> IRequestHandler<TRequest, Unit>.Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            await this.HandleAsync(request);
            return Unit.Value;
        }

        public abstract Task HandleAsync(TRequest request);
    }
}