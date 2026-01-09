namespace StorageApp.Models
{
    public class Product
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Price { get; set; } = default!;
        public int Stock { get; set; }

    }
}
