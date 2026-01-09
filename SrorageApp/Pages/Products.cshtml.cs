using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StorageApp.Data;
using StorageApp.Models;

namespace StorageApp.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ProductRepository _productRepository;

        public ProductsModel(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> Products { get; private set; } = new();

        
        [TempData]
        public string? Message { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole("admin"))
                return RedirectToPage("Error");

            Products = await _productRepository.GetProductsAsync();
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (!User.IsInRole("admin"))
                return RedirectToPage("Error");

            var deleted = await _productRepository.DeleteProductAsync(id);

            if (!deleted)
            {
                Message = "Δεν μπορεί να διαγραφεί προϊόν που έχει ενεργές παραγγελίες.";
            }

            return RedirectToPage("Products");
        }


    }
}
