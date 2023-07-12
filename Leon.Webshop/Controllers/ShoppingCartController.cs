using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Contracts.ViewModels.ShoppingCarts;
using Leon.Webshop.Logic.Helpers;
using Leon.Webshop.Logic.Services;
using Leon.Webshop.Services;
using Microsoft.AspNetCore.Mvc;

namespace Leon.Webshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly VisitorService _visitorService;
        private readonly SalesService _salesService;

        public ShoppingCartController(UnitOfWork unitOfWork, VisitorService visitorService, SalesService salesService)
        {
            _unitOfWork = unitOfWork;
            _visitorService = visitorService;
            _salesService = salesService;
        }

        public async Task<IActionResult> Details()
        {
            var sessionId = HttpContext.Session.Id;

            var visitor = await _visitorService.GetVisitor(sessionId);

            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCartsByVisitorId(visitor.Id);

            DetailsViewModel viewModel = new DetailsViewModel(shoppingCarts);

            return View(viewModel);
        }

        public async Task<IActionResult> BuyProduct(Guid productId)
        {
            var sessionId = HttpContext.Session.Id;

            var visitor = await _visitorService.GetVisitor(sessionId);

            var product = await _unitOfWork.ProductRepository.GetById(productId);

            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetByVisitorAndProduct(visitor, product);

            await _salesService.BuyProduct(product, visitor, shoppingCart);
            
            return RedirectToAction("Details");
        }

        public async Task<IActionResult> RemoveProduct(Guid productId)
        {
            var sessionId = HttpContext.Session.Id;

            var visitor = await _visitorService.GetVisitor(sessionId);

            var product = await _unitOfWork.ProductRepository.GetById(productId);

            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetByVisitorAndProduct(visitor, product);

            if (shoppingCart == null) return RedirectToAction("Details");

            await _unitOfWork.ShoppingCartRepository.Delete(shoppingCart);

            return RedirectToAction("Details");
        }
    }
}
