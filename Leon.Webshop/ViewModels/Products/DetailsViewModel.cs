using Leon.Webshop.Models;

namespace Leon.Webshop.ViewModels.Products
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
