using Leon.Webshop.Repositories;

namespace Leon.Webshop.Services
{
    public class UnitOfWork
    {
        public UnitOfWork(ProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public ProductRepository ProductRepository { get; }
    }
}
