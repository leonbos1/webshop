using Leon.Webshop.Models;
using Leon.Webshop.Services;
using Leon.Webshop.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Leon.Webshop.Controllers
{
    public class ProductController : Controller
    {
        UnitOfWork _unitOfWork;

        public ProductController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            var visitorId = HttpContext.Session.GetString("VisitorId");

            if (visitorId == null)
            {
                Visitor visitor = new Visitor
                {
                    SessionId = HttpContext.Session.Id,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.VisitorRepository.Add(visitor);

                visitorId = visitor.Id.ToString();

                HttpContext.Session.SetString("VisitorId", visitorId.ToString());
            }

            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetByVisitorId(Guid.Parse(visitorId));

            if (shoppingCart.Any(x => x.ProductId == product.Id))
            {
                var shoppingCartProduct = shoppingCart.FirstOrDefault(x => x.ProductId == product.Id);

                shoppingCartProduct.Quantity++;

                await _unitOfWork.ShoppingCartRepository.Update(shoppingCartProduct);
            }
            else
            {
                ShoppingCart shoppingCartProduct = new ShoppingCart
                {
                    ProductId = product.Id,
                    Quantity = 1,
                    VisitorId = Guid.Parse(visitorId)
                };

                await _unitOfWork.ShoppingCartRepository.Create(shoppingCartProduct);
            }

            return RedirectToAction("Index", "Product");
        }
    }
}