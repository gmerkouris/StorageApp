using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StorageApp.Data;
using StorageApp.Models;
using System;
using System.Security.Claims;

namespace StorageApp.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContext;

        public CreateOrderModel(
            OrderRepository orderRepository,
            ProductRepository productRepository,
            IHttpContextAccessor httpContext)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _httpContext = httpContext;
        }

        [BindProperty]
        public Order Order { get; set; } = new Order();

        public Product Product { get; set; } = new Product();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var userId = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToPage("Error");

            var product = await _productRepository.FindByIdAsync(id);
            if (product == null)
                return RedirectToPage("Error");

            Product = product;

         
            Order.ProductId = product.Id;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToPage("Error");

            Order.UserId = userId;

            if (string.IsNullOrWhiteSpace(Order.Id))
                Order.Id = RandomString(10);

           
            ModelState.Remove("Order.Id");
            ModelState.Remove("Order.UserId");

          
            if (!string.IsNullOrWhiteSpace(Order.ProductId))
            {
                var product = await _productRepository.FindByIdAsync(Order.ProductId);
                if (product != null) Product = product;
            }

            if (!ModelState.IsValid)
                return Page();

            var ok = await _orderRepository.AddOrderAsync(Order);
            if (!ok)
            {
                ModelState.AddModelError(string.Empty, "Η παραγγελία δεν ολοκληρώθηκε (έλεγχος αποθέματος/προϊόντος).");
                return Page();
            }

            return RedirectToPage("./Orders");
        }


        private static readonly Random random = new();

        private static string RandomString(int length)
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
