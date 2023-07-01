using Leon.Webshop.Contracts.Models;

namespace Leon.Webshop.Contracts.ViewModels.Products
{
    public class IndexViewModel
    {
        public List<Product> Products { get; set; }

        public IndexViewModel(List<Product> products)
        {
            this.Products = products;
        }
    }
}
