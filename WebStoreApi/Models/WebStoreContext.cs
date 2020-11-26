using Microsoft.EntityFrameworkCore;

namespace WebStoreApi.Models
{
    public class WebStoreContext : DbContext
    {
        public WebStoreContext(DbContextOptions<WebStoreContext> options)
            : base(options)
        {
        }

        public DbSet<WebStoreItem> WebStoreItems { get; set; }
    }
}
