using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Logic.Services
{
    public class SalesService
    {
        private readonly SalesRepository _salesRepository;

        public SalesService(SalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task BuyProduct(Product product, Visitor visitor)
        {
            var sale = new Sale
            {
                ProductId = product.Id,
                VisitorId = visitor.Id,
                Created = DateTime.Now
            };

            _salesRepository.AddSale(sale);
        }
    }
}
