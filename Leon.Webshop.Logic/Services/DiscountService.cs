using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Logic.Services
{
    public class DiscountService
    {
        private readonly UnitOfWork _unitOfWork;

        public DiscountService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ApplyDiscount(List<Product> products)
        {
            if (products == null || !products.Any())
                return;

            if (products[0].FinalPrice != 0)
                return;

            var productDiscounts = await _unitOfWork.DiscountProductRepository.GetAll();
            var discounts = await _unitOfWork.DiscountRepository.GetAll();

            foreach (var product in products)
            {
                var applicableDiscounts = GetApplicableDiscounts(productDiscounts, discounts, product.Id);

                foreach (var discount in applicableDiscounts)
                {
                    await ApplyDiscountToProduct(product, discount);
                }

                if (product.FinalPrice == 0)
                {
                    product.FinalPrice = product.Price;
                }
            }
        }

        private IEnumerable<Discount> GetApplicableDiscounts(IEnumerable<ProductDiscount> productDiscounts, IEnumerable<Discount> discounts, Guid productId)
        {
            return productDiscounts
                .Where(x => x.ProductId == productId)
                .Join(discounts, pd => pd.DiscountId, d => d.Id, (pd, d) => d);
        }

        private async Task ApplyDiscountToProduct(Product product, Discount discount)
        {
            var currentPrice = product.FinalPrice == 0 ? product.FinalPrice : product.Price;

            if (discount.Percentage > 0)
            {
                product.FinalPrice = currentPrice - (currentPrice * discount.Percentage / 100);
            }

            if (discount.Amount > 0)
            {
                product.FinalPrice = currentPrice - discount.Amount;
            }
            
            await _unitOfWork.ProductRepository.Update(product);
        }
    }
}
