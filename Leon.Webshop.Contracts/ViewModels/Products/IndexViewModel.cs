using Leon.Webshop.Contracts.Models;

namespace Leon.Webshop.Contracts.ViewModels.Products
{
    public class IndexViewModel
    {
        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public IndexViewModel(List<Product> products, List<Category> categories)
        {
            this.Products = products;
            Categories = categories;
        }
    }
}
