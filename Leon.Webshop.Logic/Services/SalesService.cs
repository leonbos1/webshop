using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Data.Repositories;
using Leon.Webshop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Logic.Services
{
    public class SalesService
    {
        private readonly UnitOfWork _uow;


        public SalesService(UnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task BuyProduct(Product product, Visitor visitor, ShoppingCart shoppingCart)
        {
            product.Stock--;

            await _uow.ProductRepository.Update(product);

            await _uow.ShoppingCartRepository.Delete(shoppingCart);

            var sale = new Sale
            {
                ProductId = product.Id,
                VisitorId = visitor.Id,
                Created = DateTime.Now
            };

            await _uow.SalesRepository.AddSale(sale);
        }
    }
}
