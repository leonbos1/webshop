using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Services;
using Leon.Webshop.Contracts.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Leon.Webshop.Logic.Helpers;

namespace Leon.Webshop.Controllers
{
    public class ProductController : Controller
    {
        UnitOfWork _unitOfWork;
        VisitorService _visitorService;

        public ProductController(UnitOfWork unitOfWork, VisitorService visitorService)
        {
            _unitOfWork = unitOfWork;
            _visitorService = visitorService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.ProductRepository.GetAll();

            IndexViewModel viewModel = new IndexViewModel(products);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);

            DetailsViewModel viewModel = new DetailsViewModel(product);

            return View(viewModel);
        }

        public async Task<IActionResult> Buy(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            if (product.Stock == 0)
            {
                return RedirectToAction("Index");
            }

            var sessionId = HttpContext.Session.Id;

            var visitor = await _visitorService.GetVisitor(sessionId);

            HttpContext.Session.SetString("VisitorId", visitor.Id.ToString());

            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetByVisitorAndProduct(visitor, product);

            if (shoppingCart == null)
            {
                ShoppingCart newShoppingCart = new ShoppingCart
                {
                    VisitorId = visitor.Id,
                    ProductId = product.Id,
                    Quantity = 1
                };

                await _unitOfWork.ShoppingCartRepository.Create(newShoppingCart);
            }
            else
            {
                shoppingCart.Quantity++;

                await _unitOfWork.ShoppingCartRepository.Update(shoppingCart);
            }

            product.Stock--;

            await _unitOfWork.ProductRepository.Update(product);

            return RedirectToAction("Index");
        }
    }
}