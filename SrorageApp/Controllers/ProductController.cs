using Microsoft.AspNetCore.Mvc;
using SrorageApp.Data;
using SrorageApp.Models;

namespace SrorageApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> ViewProducts()
        {
            List<Product> products = await _productRepository.GetProductsAsync();
            return View(products);
        }
    }
}

