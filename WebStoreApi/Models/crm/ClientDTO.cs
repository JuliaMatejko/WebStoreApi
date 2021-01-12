namespace WebStoreApi.Models.crm
{
    public class ClientDTO
    {
        public ulong Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Address Address { get; set; }

    }
}
