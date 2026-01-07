using Microsoft.AspNetCore.Mvc.RazorPages;
using SrorageApp.Data;
using SrorageApp.Models;

namespace SrorageApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductRepository _productRepository;

        public List<Product> Products { get; private set; } = new();

        public IndexModel(ILogger<IndexModel> logger, ProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task OnGetAsync()
        {
            Products = await _productRepository.GetProductsAsync();
        }
    }
}
