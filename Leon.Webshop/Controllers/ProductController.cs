using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Services;
using Leon.Webshop.Contracts.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Leon.Webshop.Logic.Helpers;
using Leon.Webshop.Logic.Services;

namespace Leon.Webshop.Controllers
{
    public class ProductController : Controller
    {
        UnitOfWork _unitOfWork;
        VisitorService _visitorService;
        ProductService _productService;
        SalesService _salesService;

        public ProductController(UnitOfWork unitOfWork, VisitorService visitorService, ProductService productService, SalesService salesService)
        {
            _unitOfWork = unitOfWork;
            _visitorService = visitorService;
            _productService = productService;
            _salesService = salesService;
        }

        public async Task<IActionResult> Index()
        {
            var categoryId = Request.Query["categoryId"].ToString();

            List<Product> products;

            List<Category> categories;

            IndexViewModel viewModel;

            if (string.IsNullOrEmpty(categoryId))
            {
                products = await _productService.GetProducts();

                categories = await _unitOfWork.CategoryRepository.GetAll();

                viewModel = new IndexViewModel(products, categories);

                return View(viewModel);
            }

            var validGuid = Guid.TryParse(categoryId, out Guid guid);

            if (!validGuid)
            {
                return NotFound();
            }

            var category = await _unitOfWork.CategoryRepository.GetById(guid);

            if (category == null && categoryId != string.Empty)
            {
                return NotFound();
            }

            var allProducts = await _productService.GetProducts();

            products = allProducts.Where(x => x.CategoryId == guid).ToList();

            categories = await _unitOfWork.CategoryRepository.GetAll();

            viewModel = new IndexViewModel(products, categories);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetProductById(id);

            DetailsViewModel viewModel = new DetailsViewModel(product);

            return View(viewModel);
        }

        public async Task<IActionResult> Buy(Guid id)
        {
            var product = await _productService.GetProductById(id);

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

            await _salesService.BuyProduct(product, visitor);

            await _unitOfWork.ProductRepository.Update(product);

            return RedirectToAction("Index");
        }
    }
}