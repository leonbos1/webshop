using Leon.Webshop.Contracts.Models;

namespace Leon.Webshop.Contracts.ViewModels.Products
{
    public class DetailsViewModel
    {
        public Product Product { get; set; }

        public DetailsViewModel(Product product)
        {
            this.Product = product;
        }
    }
}
