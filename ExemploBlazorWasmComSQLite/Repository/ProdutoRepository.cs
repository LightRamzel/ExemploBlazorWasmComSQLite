using ExemploBlazorWasmComSQLite.Data;
using ExemploBlazorWasmComSQLite.Interfaces;
using ExemploBlazorWasmComSQLite.Model;
using Microsoft.EntityFrameworkCore;

namespace ExemploBlazorWasmComSQLite.Repository;

public class ProdutoRepository(IAppDbContextFactory dbContextFactory) : IProdutoRepository
{
    private readonly IAppDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task<Produto> AddProdutoAsync(string descricao, decimal saldoEstoque)
    {
        using var dbContext = await _dbContextFactory.CreateAppDbContextAsync();
        var produto = new Produto(descricao, saldoEstoque);
        dbContext.Produto.Add(produto);
        await dbContext.SaveChangesAsync();
        return produto;
    }

    public async Task EditProdutoAsync(int id, string descricao, decimal saldoEstoque)
    {
        using var dbContext = await _dbContextFactory.CreateAppDbContextAsync();
        var produto = await GetProdutoByIdAsync(dbContext, id);
        produto.Descricao = descricao;
        produto.SaldoEstoque = saldoEstoque;
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> ActivateInativate(int id)
    {
        using var dbContext = await _dbContextFactory.CreateAppDbContextAsync();
        var produto = await GetProdutoByIdAsync(dbContext, id);
        produto.Ativo = !produto.Ativo;
        await dbContext.SaveChangesAsync();
        return produto.Ativo;
    }

    public async Task<Produto[]> GetProdutosAsync()
    {
        using var dbContext = await _dbContextFactory.CreateAppDbContextAsync();
        return await dbContext.Produto.ToArrayAsync();
    }

    private static async Task<Produto> GetProdutoByIdAsync(AppDbContext dbContext, int id) =>
        await dbContext.Produto.FindAsync(id) ?? throw new Exception("Produto não localizado");
}
