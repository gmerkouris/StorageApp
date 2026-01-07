using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SrorageApp.Data;
using SrorageApp.Models;
using System.Security.Claims;

namespace SrorageApp.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly OrderRepository _ordersRepository;
        private readonly IHttpContextAccessor _http;

        public OrdersModel(OrderRepository ordersRepository, IHttpContextAccessor http)
        {
            _ordersRepository = ordersRepository;
            _http = http;
        }

        public List<Order> Orders { get; private set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToPage("Error");

            Orders = await _ordersRepository.GetOrdersWithUserAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var userId = _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToPage("Error");

            await _ordersRepository.DeleteOrderAsync(id);

            return RedirectToPage("Orders");
        }
    }
}
