using ExemploBlazorWasmComSQLite.Model;

namespace ExemploBlazorWasmComSQLite.Interfaces;

public interface IProdutoRepository
{
    Task<Produto> AddProdutoAsync(string descricao, decimal saldoEstoque);
    Task EditProdutoAsync(int id, string descricao, decimal saldoEstoque);
    Task<bool> ActivateInativate(int id);
    Task<Produto[]> GetProdutosAsync();
}
