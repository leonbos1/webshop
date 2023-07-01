using Leon.Webshop.Contracts.Models;

namespace Leon.Webshop.Contracts.ViewModels.ShoppingCarts
{
    public class DetailsViewModel
    {
        public List<ShoppingCart> shoppingCarts;

        public DetailsViewModel(List<ShoppingCart> shoppingCarts)
        {
            this.shoppingCarts = shoppingCarts;
        }
    }
}
