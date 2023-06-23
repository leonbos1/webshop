using Leon.Webshop.Models;

namespace Leon.Webshop.ViewModels.Products
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
