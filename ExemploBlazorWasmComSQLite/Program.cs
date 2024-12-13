using ExemploBlazorWasmComSQLite;
using ExemploBlazorWasmComSQLite.Data;
using ExemploBlazorWasmComSQLite.Interfaces;
using ExemploBlazorWasmComSQLite.Repository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseSqlite("Data Source=databaseapp.sqlite3"));
builder.Services.AddSingleton<IAppDbContextFactory, SynchronizedAppDbContextFactory>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
