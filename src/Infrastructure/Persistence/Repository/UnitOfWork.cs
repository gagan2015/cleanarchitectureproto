using TopupPortal.Application.Common.Interfaces;

namespace TopupPortal.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IProductRepository productRepository)
        {
            Products = productRepository;
        }
        public IProductRepository Products { get; }
    }
}
