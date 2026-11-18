using Microsoft.EntityFrameworkCore;
using StorageAPI.Models;

public class StorageAPIContext(DbContextOptions<StorageAPIContext> options) : DbContext(options)
{
    public DbSet<Product> Product { get; set; } = default!;
}
