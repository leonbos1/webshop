using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Services;

namespace Leon.Webshop.Logic.Services
{
    public class DiscountService
    {
        private readonly UnitOfWork _unitOfWork;

        public DiscountService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ApplyDiscounts(List<Product> products)
        {
            foreach (var product in products)
            {
                var discounts = await _unitOfWork.DiscountProductRepository.GetAllByProductId(product.Id);

                if (discounts.Count > 0)
                {
                    foreach (var discount in discounts)
                    {
                        discount.Discount = await _unitOfWork.DiscountRepository.GetById(discount.DiscountId);

                        if (discount.Discount.Percentage > 0)
                        {
                            product.Price = product.Price - (product.Price * (discount.Discount.Percentage / 100));
                        }

                        if (discount.Discount.Amount > 0)
                        {
                            product.Price = product.Price - discount.Discount.Amount;
                        }
                    }
                }
            }
        }
    }
}
