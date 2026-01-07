using Microsoft.AspNetCore.Identity;

namespace SrorageApp.Models
{
    public class Order
    {
        public string Id { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public IdentityUser? User { get; set; }

        public string? ProductId { get; set; }

        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? FullName { get; set; }


    }

    }
