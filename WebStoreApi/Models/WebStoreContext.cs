using Microsoft.EntityFrameworkCore;
using WebStoreApi.Models.crm;

namespace WebStoreApi.Models
{
    public class WebStoreContext : DbContext
    {
        public WebStoreContext(DbContextOptions<WebStoreContext> options)
            : base(options)
        {
        }

        public DbSet<WebStoreItem> WebStoreItems { get; set; }

        public DbSet<Client> Client { get; set; }
    }
}
