namespace ExemploBlazorWasmComSQLite.Model;

public class Produto
{
    public Produto()
    {
        
    }

    public Produto(string descricao, decimal saldoEstoque)
    {
        Descricao = descricao;
        SaldoEstoque = saldoEstoque;
    }

    //public Produto(int id, string descricao, decimal saldoEstoque)
    //{
    //    Id = id;
    //    Descricao = descricao;
    //    SaldoEstoque = saldoEstoque;
    //}

    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal SaldoEstoque { get; set; } = decimal.Zero;
    public bool Ativo { get; set; } = true;
    public DateTime DataCadastro { get; set; } = DateTime.Now;
}
