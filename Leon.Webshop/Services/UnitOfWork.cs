using Leon.Webshop.Repositories;

namespace Leon.Webshop.Services
{
    public class UnitOfWork
    {
        public UnitOfWork(ProductRepository productRepository, ShoppingCartRepository shoppingCartRepository, VisitorRepository visitorRepository)
        {
            ProductRepository = productRepository;
            ShoppingCartRepository = shoppingCartRepository;
            VisitorRepository = visitorRepository;
        }

        public ProductRepository ProductRepository { get; }

        public ShoppingCartRepository ShoppingCartRepository { get; }

        public VisitorRepository VisitorRepository { get; }
    }
}
