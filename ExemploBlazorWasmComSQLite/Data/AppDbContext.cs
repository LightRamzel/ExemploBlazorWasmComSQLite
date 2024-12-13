using ExemploBlazorWasmComSQLite.Model;
using Microsoft.EntityFrameworkCore;

namespace ExemploBlazorWasmComSQLite.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Produto> Produto { get; set; } = null!;
}
