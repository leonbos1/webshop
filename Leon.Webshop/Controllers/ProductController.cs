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

        public async Task<IActionResult> Details(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);

            DetailsViewModel viewModel = new DetailsViewModel(product);

            return View(viewModel);
        }

        public async Task<IActionResult> Buy(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);

            DetailsViewModel viewModel = new DetailsViewModel(product);

            return View(viewModel);
        }
    }
}