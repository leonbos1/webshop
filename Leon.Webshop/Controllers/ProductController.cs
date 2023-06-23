using Leon.Webshop.Models;
using Leon.Webshop.Services;
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

            return View(products[0]);
        }
    }
}
