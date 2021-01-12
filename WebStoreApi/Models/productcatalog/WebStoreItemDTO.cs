namespace WebStoreApi.Models
{
    public class WebStoreItemDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDiscounted { get; set; }
        public string Picture { get; set; }
    }
}
