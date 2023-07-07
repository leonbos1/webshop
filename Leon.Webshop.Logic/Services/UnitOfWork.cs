using Leon.Webshop.Data.Repositories;

namespace Leon.Webshop.Services
{
    public class UnitOfWork
    {
        public UnitOfWork(ProductRepository productRepository, ShoppingCartRepository shoppingCartRepository, VisitorRepository visitorRepository, CategoryRepository categoryRepository, DiscountProductRepository discountProductRepository, DiscountRepository discountRepository)
        {
            ProductRepository = productRepository;
            ShoppingCartRepository = shoppingCartRepository;
            VisitorRepository = visitorRepository;
            CategoryRepository = categoryRepository;
            DiscountProductRepository = discountProductRepository;
            DiscountRepository = discountRepository;
        }

        public ProductRepository ProductRepository { get; }

        public ShoppingCartRepository ShoppingCartRepository { get; }

        public VisitorRepository VisitorRepository { get; }

        public CategoryRepository CategoryRepository { get; }

        public DiscountRepository DiscountRepository { get; }

        public DiscountProductRepository DiscountProductRepository { get; }
    }
}
