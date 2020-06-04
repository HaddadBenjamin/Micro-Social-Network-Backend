using DiabloII.Domain.Commands.Domains.Items;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Handlers.Bases;

namespace DiabloII.Infrastructure.Handlers.Domains
{
    public class ItemCommandHandler : CommandHandler<ResetItemsCommand>
    {
        private readonly IItemRepository _repository;

        private readonly ApplicationDbContext _dbContext;

        public ItemCommandHandler(IItemRepository repository, ApplicationDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public override void Handle(ResetItemsCommand command)
        {
            _repository.ResetTheItems(command.Items, command.ItemProperties);

            _dbContext.SaveChanges();
        }
    }
}