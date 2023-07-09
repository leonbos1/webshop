using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Logic.Services
{
    public class ProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly DiscountService _discountService;

        public ProductService(UnitOfWork unitOfWork, DiscountService discountService)
        {
            _unitOfWork = unitOfWork;
            _discountService = discountService;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _unitOfWork.ProductRepository.GetAll();

            await _discountService.ApplyDiscount(products);

            return products;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);

            await _discountService.ApplyDiscount(new List<Product> { product });

            return product;
        }
    }
}
