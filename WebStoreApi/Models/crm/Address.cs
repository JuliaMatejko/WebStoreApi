namespace WebStoreApi.Models.crm
{
    public class Address
    {
        public ulong Id { get; set; }
        public string Country { get; set; }
        public string Locality { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string ApartmentNumber { get; set; }
    }
}
