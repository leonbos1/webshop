using Leon.Webshop.Contracts.ViewModels.ShoppingCarts;
using Leon.Webshop.Logic.Helpers;
using Leon.Webshop.Services;
using Microsoft.AspNetCore.Mvc;

namespace Leon.Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly VisitorService _visitorService;

        public ShoppingCartController(UnitOfWork unitOfWork, VisitorService visitorService)
        {
            _unitOfWork = unitOfWork;
            _visitorService = visitorService;
        }

        public async Task<IActionResult> Details()
        {
            var sessionId = HttpContext.Session.Id;

            var visitor = await _visitorService.GetVisitor(sessionId);

            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCartsByVisitorId(visitor.Id);

            DetailsViewModel viewModel = new DetailsViewModel(shoppingCarts);
            
            return View(viewModel);
        }
    }
}
