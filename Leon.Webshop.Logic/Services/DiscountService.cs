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
            var productDiscounts = await _unitOfWork.DiscountProductRepository.GetAll();
            var discounts = await _unitOfWork.DiscountRepository.GetAll();

            foreach (var product in products)
            {
                var applicableDiscounts = GetApplicableDiscounts(productDiscounts, discounts, product.Id);

                foreach (var discount in applicableDiscounts)
                {
                    ApplyDiscountToProduct(product, discount);
                }
            }
        }

        private IEnumerable<Discount> GetApplicableDiscounts(IEnumerable<ProductDiscount> productDiscounts, IEnumerable<Discount> discounts, Guid productId)
        {
            return productDiscounts
                .Where(x => x.ProductId == productId)
                .Join(discounts, pd => pd.DiscountId, d => d.Id, (pd, d) => d);
        }

        private void ApplyDiscountToProduct(Product product, Discount discount)
        {
            if (discount.Percentage > 0)
            {
                product.Price -= (product.Price * (discount.Percentage / 100));
            }

            if (discount.Amount > 0)
            {
                product.Price -= discount.Amount;
            }
        }
    }
}
