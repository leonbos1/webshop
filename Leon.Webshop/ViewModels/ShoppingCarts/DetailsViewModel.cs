using Leon.Webshop.Models;

namespace Leon.Webshop.ViewModels.ShoppingCarts
{
    public class DetailsViewModel
    {
        public DetailsViewModel(ShoppingCart shoppingCart)
        {
            ShoppingCart = shoppingCart;
        }

        public ShoppingCart ShoppingCart { get; }
    }
}
