using System.Threading;
using System.Threading.Tasks;
using DiabloII.Domain.Commands.Items;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.DbContext;
using MediatR;

namespace DiabloII.Infrastructure.Handlers
{
    public class ItemCommandHandler : IRequestHandler<ResetItemsCommand>
    {
        private readonly IItemRepository _repository;

        private readonly ApplicationDbContext _dbContext;

        public ItemCommandHandler(IItemRepository repository, ApplicationDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ResetItemsCommand command, CancellationToken cancellationToken = default)
        {
            _repository.ResetTheItems(command.Items, command.ItemProperties);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}