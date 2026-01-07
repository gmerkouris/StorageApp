using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SrorageApp.Data;
using SrorageApp.Models;

namespace SrorageApp.Pages
{
    public class CreateProductModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContext;

        public CreateProductModel(ProductRepository productRepository, IHttpContextAccessor httpContext)
        {
            _productRepository = productRepository;
            _httpContext = httpContext;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public IActionResult OnGet()
        {
            if (User.IsInRole("admin"))
                return Page();

            return RedirectToPage("Error");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.IsInRole("admin"))
                return RedirectToPage("Error");

            if (!ModelState.IsValid)
                return Page();

            await _productRepository.AddProductsAsync(Product);

            return RedirectToPage("./Products");
        }
    }
}
