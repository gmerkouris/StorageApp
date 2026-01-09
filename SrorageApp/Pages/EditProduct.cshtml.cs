using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StorageApp.Data;
using StorageApp.Models;

namespace StorageApp.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly ProductRepository _productRepository;

        public EditProductModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!User.IsInRole("admin"))
                return RedirectToPage("Error");

            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
                return RedirectToPage("Error");

            Product = product;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!User.IsInRole("admin"))
                return RedirectToPage("Error");

            if (!ModelState.IsValid)
                return Page();

            await _productRepository.UpdateProductsAsync(Product);
            return RedirectToPage("./Products");
        }
    }
}
