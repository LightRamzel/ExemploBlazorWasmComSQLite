using ExemploBlazorWasmComSQLite.Data;

namespace ExemploBlazorWasmComSQLite.Interfaces;

public interface IAppDbContextFactory
{
    Task<AppDbContext> CreateAppDbContextAsync();
}
