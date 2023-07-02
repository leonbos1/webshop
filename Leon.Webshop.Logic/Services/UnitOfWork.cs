using Leon.Webshop.Data.Repositories;

namespace Leon.Webshop.Services
{
    public class UnitOfWork
    {
        public UnitOfWork(ProductRepository productRepository, ShoppingCartRepository shoppingCartRepository, VisitorRepository visitorRepository, CategoryRepository categoryRepository)
        {
            ProductRepository = productRepository;
            ShoppingCartRepository = shoppingCartRepository;
            VisitorRepository = visitorRepository;
            CategoryRepository = categoryRepository;
        }

        public ProductRepository ProductRepository { get; }

        public ShoppingCartRepository ShoppingCartRepository { get; }

        public VisitorRepository VisitorRepository { get; }

        public CategoryRepository CategoryRepository { get; }
    }
}
