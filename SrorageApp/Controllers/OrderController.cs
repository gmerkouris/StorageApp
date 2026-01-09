using Microsoft.AspNetCore.Mvc;
using StorageApp.Data;
using StorageApp.Models;

namespace StorageApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderRepository _ordersRepository;

        public OrderController(OrderRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _ordersRepository.GetOrdersWithUserAsync();
            return View(orders);
        }
    }
}
