using Leon.Webshop.Services;
using Microsoft.AspNetCore.Mvc;

namespace Leon.Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public ShoppingCartController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(Guid visitorId)
        {
            var shoppingCart = _unitOfWork.ShoppingCartRepository.GetByVisitorId(visitorId);

            return View(shoppingCart);
        }


    }
}
